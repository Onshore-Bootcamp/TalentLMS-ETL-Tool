namespace EverymanETL.App_Start
{
    using EverymanETL.Custom;
    using EverymanETL.DataProviders;
    using EverymanETL.Models.API;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public static class Configure
    {
        public static void APIs()
        {
            API.SetSource("");
            API.SetSecretKey("");
            APIResolver<Branch>.RegisterDataProvider(() => { return API.SendRequest<HashSet<Branch>>("branches"); });
            APIResolver<Category>.RegisterDataProvider(() => { return API.SendRequest<HashSet<Category>>("categories"); });
            APIResolver<Course>.RegisterDataProvider(() => { return API.SendRequest<HashSet<Course>>("courses"); });
            APIResolver<Group>.RegisterDataProvider(() => { return API.SendRequest<HashSet<Group>>("groups"); });
            APIResolver<User>.RegisterDataProvider(() => { return API.SendRequest<HashSet<User>>("users"); });

            //Units
            APIResolver<Unit>.RegisterDataProvider(() =>
            {
                HashSet<Unit> allUnits = new HashSet<Unit>();
                HashSet<Course> allCourses = API.SendRequest<HashSet<Course>>("courses");
                foreach (Course course in allCourses)
                {
                    //Go get units
                    Course courseDetails = API.SendRequest<Course>("courses/id:" + course.Id.ToString());
                    foreach (Unit unit in courseDetails.units)
                    {
                        unit.course_id = course.Id;
                        allUnits.Add(unit);
                    }
                }
                return allUnits;
            });

            //Branch Courses
            APIResolver<BranchCourse>.RegisterDataProvider(() =>
            {
                HashSet<BranchCourse> allBranchCourseAssignments = new HashSet<BranchCourse>();
                HashSet<Branch> allBranches = APIResolver<Branch>.ResolveData();

                foreach (Branch branch in allBranches)
                {
                    Branch branchDetails = API.SendRequest<Branch>("branches/id:" + branch.Id.ToString());
                    foreach (Course assignedCourse in branchDetails.Courses)
                    {
                        allBranchCourseAssignments.Add(new BranchCourse(0, branch.Id, assignedCourse.Id));
                    }
                }
                return allBranchCourseAssignments;
            });

            //Branch Users
            APIResolver<BranchUser>.RegisterDataProvider(() =>
            {
                HashSet<BranchUser> allBranchUserAssignments = new HashSet<BranchUser>();
                HashSet<Branch> allBranches = APIResolver<Branch>.ResolveData();

                foreach (Branch branch in allBranches)
                {
                    Branch branchDetails = API.SendRequest<Branch>("branches/id:" + branch.Id.ToString());
                    foreach (User assignedUser in branchDetails.Users)
                    {
                        allBranchUserAssignments.Add(new BranchUser(0, branch.Id, assignedUser.Id));
                    }
                }
                return allBranchUserAssignments;
            });

            //Group Users
            APIResolver<GroupUser>.RegisterDataProvider(() =>
            {
                HashSet<GroupUser> allGroupUserAssignments = new HashSet<GroupUser>();
                HashSet<Group> allGroups = APIResolver<Group>.ResolveData();

                foreach (Group group in allGroups)
                {
                    Group groupDetails = API.SendRequest<Group>("groups/id:" + group.Id.ToString());
                    foreach (User assignedUser in groupDetails.Users)
                    {
                        allGroupUserAssignments.Add(new GroupUser(0, group.Id, assignedUser.Id));
                    }
                }
                return allGroupUserAssignments;
            });

            //This is new as of 8-20-2018
            APIResolver<GroupCourse>.RegisterDataProvider(() =>
            {
                HashSet<GroupCourse> allCourseGroupAssignments = new HashSet<GroupCourse>();
                HashSet<Group> allGroups = APIResolver<Group>.ResolveData();

                foreach (Group group in allGroups)
                {
                    Group groupDetails = API.SendRequest<Group>("groups/id:" + group.Id.ToString());
                    foreach (Course assignedCourse in groupDetails.courses)
                    {
                        allCourseGroupAssignments.Add(new GroupCourse(0, group.Id, assignedCourse.Id));
                    }
                }
                return allCourseGroupAssignments;
            });

            //Course Status - UserCourseDetails
            APIResolver<UserCourseDetails>.RegisterDataProvider(() =>
            {
                HashSet<UserCourseDetails> allUserCourseStatuses = new HashSet<UserCourseDetails>();
                HashSet<Course> allCourses = APIResolver<Course>.ResolveData();

                foreach (Course course in allCourses)
                {
                    //Since DB was just updated, pull from DB instead.
                    Course courseDetails = API.SendRequest<Course>("courses/id:" + course.Id.ToString());
                    foreach (User assignedUser in courseDetails.Users)
                    {
                        UserCourseDetails userStatus = API.SendRequest<UserCourseDetails>($"getuserstatusincourse/course_id:{ course.Id },user_id:{ assignedUser.Id }");

                        userStatus.CourseId = course.Id;
                        userStatus.UserId = assignedUser.Id;
                        allUserCourseStatuses.Add(userStatus);
                    }
                }
                return allUserCourseStatuses;
            });

            //Unit Completion - UserCourseDetails
            APIResolver<UnitCompletion>.RegisterDataProvider(() =>
            {
                HashSet<UnitCompletion> allUsersUnitCompletion = new HashSet<UnitCompletion>();
                HashSet<Course> allCourses = APIResolver<Course>.ResolveData();

                HashSet<UserCourseDetails> allCourseDetails = ProviderResolver.ResolveDatabaseProvider<UserCourseDetails>().ViewAll();

                foreach (Course course in allCourses)
                {
                    Course courseDetails = API.SendRequest<Course>("courses/id:" + course.Id.ToString());
                    foreach (User assignedUser in courseDetails.Users)
                    {
                        UserCourseDetails userStatus = API.SendRequest<UserCourseDetails>($"getuserstatusincourse/course_id:{ course.Id },user_id:{ assignedUser.Id }");
                        foreach (UnitCompletion userUnitStatus in userStatus.Units)
                        {
                            foreach (UserCourseDetails ucd in allCourseDetails)
                            {
                                if (assignedUser.Id == ucd.UserId && course.Id == ucd.CourseId)
                                {
                                    userUnitStatus.UserCourseDetailsId = ucd.UserCourseDetailsId;
                                    userUnitStatus.UserId = assignedUser.Id;
                                    userUnitStatus.CourseId = course.Id;

                                    allUsersUnitCompletion.Add(userUnitStatus);
                                }
                            }
                        }
                    }
                }
                return allUsersUnitCompletion;
            });
        }

        public static void Providers()
        {
            string logPath = string.Join("/", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LogFile.txt");
            string connectionString = "";

            ProviderResolver.RegisterDatabaseProvider<Branch>(() => { return new BranchProvider(connectionString, logPath); });
            ProviderResolver.RegisterDatabaseProvider<Course>(() => { return new CourseProvider(connectionString, logPath); });
            ProviderResolver.RegisterDatabaseProvider<User>(() => { return new UserProvider(connectionString, logPath); });
            ProviderResolver.RegisterDatabaseProvider<Unit>(() => { return new UnitProvider(connectionString, logPath); });
            ProviderResolver.RegisterDatabaseProvider<Group>(() => { return new GroupProvider(connectionString, logPath); });
            ProviderResolver.RegisterDatabaseProvider<Category>(() => { return new CategoryProvider(connectionString, logPath); });

            //Create provider for BranchCourses
            ProviderResolver.RegisterDatabaseProvider<BranchCourse>(() => { return new BranchCourseProvider(connectionString, logPath); });
            //Create provider for BranchUsers
            ProviderResolver.RegisterDatabaseProvider<BranchUser>(() => { return new BranchUserProvider(connectionString, logPath); });
            //Create provider for GroupUsers
            ProviderResolver.RegisterDatabaseProvider<GroupUser>(() => { return new GroupUserProvider(connectionString, logPath); });
            //Create provider for GroupCourses new As of 8-20-2018
            ProviderResolver.RegisterDatabaseProvider<GroupCourse>(() => { return new GroupCourseProvider(connectionString, logPath); });
            //Create provider for CourseDetails
            ProviderResolver.RegisterDatabaseProvider<UserCourseDetails>(() => { return new UserCourseDetailsProvider(connectionString, logPath); });
            //Create provider for UnitCompletion
            ProviderResolver.RegisterDatabaseProvider<UnitCompletion>(() => { return new UnitCompletionProvider(connectionString, logPath); });
        }
    }
}
