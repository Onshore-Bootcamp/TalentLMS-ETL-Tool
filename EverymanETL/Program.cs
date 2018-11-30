namespace EverymanETL
{
    using EverymanETL.App_Start;
    using EverymanETL.Custom;
    using EverymanETL.DataProviders;
    using EverymanETL.Models.API;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Net;

    class Program
    {
        private static Func<int, int, bool> IsNow = (hour, min) => { return DateTime.Now.ToLocalTime().Hour.Equals(hour) && DateTime.Now.ToLocalTime().Minute.Equals(min); };
        
        private static bool UserOverride(ConsoleKey keyToCheck = ConsoleKey.Enter)
        {
            bool userOverride = false;
            if (Console.KeyAvailable)
            {
                userOverride = Console.ReadKey(true).Key == keyToCheck;
            }
            return userOverride;
        }

        private static Func<decimal, decimal, bool> PercentageCallback => (current, total) =>
        {
            int initial = Console.CursorLeft;
            Console.Write($"{ (current / total).ToString("P2") }%");
            Console.SetCursorPosition(initial, Console.CursorTop);
            return true;
        };

        private static void Init()
        {
            Console.CursorVisible = false;
            Configure.APIs();
            Configure.Providers();
        }

        static void Main(string[] args)
        {
            Init();

            while (true)
            {
                Console.Clear();
                //Times  6:00 am  7:30 am 9:00 am  10:30 am  12:00 noon  1:30 pm  3:00 PM  4:30 pm
                if (UserOverride() || IsNow(6, 00) || IsNow(7, 30) || IsNow(9, 00) || IsNow(10, 30) || IsNow(12, 00) || IsNow(14, 00) || IsNow(15, 00) || IsNow(16, 30))
                {
                    try
                    {
                        UpdateAllTables();
                    }
                    catch (WebException webEx)
                    {

                    }
                    catch (SqlException sqlEx)
                    {

                    }
                }
            }
        }

        private static void UpdateAllTables()
        {
            UpdateTable<Category>();
            UpdateTable<User>();
            UpdateTable<Group>();
            UpdateTable<Branch>();
            UpdateTable<Course>();
            UpdateTable<BranchCourse>();
            UpdateTable<BranchUser>();
            UpdateTable<GroupUser>();
            UpdateTable<GroupCourse>();
            UpdateTable<Unit>();
            UpdateTable<UserCourseDetails>();
            UpdateTable<UnitCompletion>();
            //Console.SetCursorPosition(0, 2);
        }

        private static void UpdateTable<T>()
        {
            string name = typeof(T).Name.Replace(typeof(T).Namespace, "");
            //Obtain reference to data provider registered to this type.
            IDataProvider<T> databaseProvider = ProviderResolver.ResolveDatabaseProvider<T>();

            //Call to DB to get all db items.
            HashSet<T> dbSource = databaseProvider.ViewAll();

            //Call to API to get current items.
            HashSet<T> apiSource = APIResolver<T>.ResolveData();

            //Create work order from items using the comparer method to check for equality.
            WorkOrder<T> workOrder = CreateWorkOrder(dbSource, apiSource);

            Console.WriteLine($"{ new string('-', 5) } { name } { new string('-', 5) }");

            Console.Write("Update ");
            Update(workOrder, PercentageCallback);
            Console.Write("Create ");
            Create(workOrder, PercentageCallback);
        }

        private static WorkOrder<T> CreateWorkOrder<T>(HashSet<T> dbSource, HashSet<T> apiSource)
        {
            //Whats been deleted
            //>> Where db source does not have one in current api source.
            //What to update?
            //>> What the db has that hasn't been deleted.
            //Whats been added?
            //>> what the API has that the DB doesn't.
            WorkOrder<T> workOrder = new WorkOrder<T>();
            foreach (T dbRecord in dbSource)
            {
                if (!EqualityComparer<T>.Default.Equals(dbRecord, default(T)))
                {
                    T tempReference = default(T);
                    bool foundInAPI = false;
                    foreach (T apiRecord in apiSource)
                    {
                        //if (predicate(dbRecord, apiRecord))
                        if (dbRecord.Equals(apiRecord))
                        {
                            foundInAPI = true;
                            tempReference = apiRecord;
                            break;
                        }
                    }
                    if (foundInAPI)
                    {
                        workOrder.UpdateOrder.Add(tempReference);
                    }
                    else
                    {
                        workOrder.DeletionOrder.Add(dbRecord);
                    }
                }
            }

            foreach (T apiRecord in apiSource)
            {
                bool foundInDatabase = false;
                if (!EqualityComparer<T>.Default.Equals(apiRecord, default(T)))
                {
                    foreach (T dbRecord in dbSource)
                    {
                        //if (predicate(dbRecord, apiRecord))
                        if (apiRecord.Equals(dbRecord))
                        {
                            foundInDatabase = true;
                            break;
                        }
                    }
                    if (!foundInDatabase)
                    {
                        workOrder.CreationOrder.Add(apiRecord);
                    }
                }
            }
            return workOrder;
        }

        public static int Create<T>(WorkOrder<T> order, Func<decimal, decimal, bool> callback)
        {
            int current = 0;
            int successful = 0;
            int count = order.CreationOrder.Count;

            string name = typeof(T).Name.Replace(typeof(T).Namespace, "");

            if (count > 0)
            {
                IDataProvider<T> dataProvider = ProviderResolver.ResolveDatabaseProvider<T>();

                foreach (T item in order.CreationOrder)
                {
                    bool created = dataProvider.Create(item);

                    if (created)
                        successful++;

                    callback(current, count);
                }

            }
            Console.WriteLine($"{ name }: { successful } / { count }");
            return successful;
        }

        private static int Update<T>(WorkOrder<T> order, Func<decimal, decimal, bool> callback)
        {
            int current = 0;
            int successful = 0;
            int count = order.UpdateOrder.Count;
            string name = typeof(T).Name.Replace(typeof(T).Namespace, "");
            if (count > 0)
            {
                IDataProvider<T> dataProvider = ProviderResolver.ResolveDatabaseProvider<T>();

                foreach (T item in order.UpdateOrder)
                {
                    bool updated = dataProvider.Update(item);

                    if (updated)
                        successful++;

                    current++;
                    callback(current, count);
                }
            }
            Console.WriteLine($"{ name }: { successful } / { count }");
            return successful;
        }

        private static int Delete<T>(WorkOrder<T> order, Func<decimal, decimal, bool> callback)
        {
            int current = 0;
            int successful = 0;
            int count = order.DeletionOrder.Count;
            string name = typeof(T).Name.Replace(typeof(T).Namespace, "");
            if (count > 0)
            {
                IDataProvider<T> dataProvider = ProviderResolver.ResolveDatabaseProvider<T>();

                foreach (T item in order.DeletionOrder)
                {
                    bool deleted = dataProvider.Delete(item);

                    if (deleted)
                        successful++;

                    callback(current, count);
                }
            }
            Console.WriteLine($"{ name }: { successful } / { count }");
            return successful;
        }

        private static void PrintInColor(ConsoleColor color, string message)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = prevColor;
        }
    }
}
