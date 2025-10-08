using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

[McpServerPromptType]
public static class McpPrompts
{
    // ===== System Guidance =====
    [McpServerPrompt, Description("System instructions for working with the Class platform API.")]
    public static ChatMessage SystemGuidance() => new(ChatRole.System,
        @"You are an agent that controls a Class's developer platform called 'Class' and also provides assistance on how to use the platform.

Class is a software platform for virtual meetings and webinars, offering video and audio conferencing, screen sharing, and chat features to connect people remotely.

With Class's API, you can perform actions directly on the platform such as:

* Create a Class
* Get All Classes Info
* Get Single Class Info
* Update a Class
* Remove a Class
* Create an Enrollment
* Get Enrollments
* Update an Enrollment
* Remove an Enrollment
* Get a Launch Link
* Get Attendance Report
* Add a Class Date
* Get Class Dates
* Remove a Class Date
* Create or Update a User
* Get All Users
* Get a User
* Remove a User
* Convert a Date to a Unix Timestamp

General guidance:

* If you need help, please ask a question.
* IF you need First Name, Last Name for user/instructor and you have email, please use function GetByEmailId to get it.
* If you need time zone, use UTC time zone by default.
* If you need to convert a date to a unix timestamp, use the function ConvertToUnixTimeSeconds.
* When you create new class and you need ext_class_id, please copy value from class name.
    For example:
    Class name: 1234567890
    ext_class_id: 1234567890
* If you need display name, please use function GetByEmailId and use user name as display name.
    For example:
    User email: user@example.com
    User name from API: Raman Papou
    display_name: Raman Papou
* Show clickable URLs for slack
    For example: https://www.google.com
    Show as: <https://www.google.com>
* Return links, URL in clickable format, NOT IN MARKDOWN FORMAT");

    // ===== Classes =====
    [McpServerPrompt, Description("Create a class with instructor, schedule, and duration.")]
    public static ChatMessage CreateClass(
        [Description("The class title/name")] string className,
        [Description("Instructor email address")] string instructorEmail,
        [Description("Schedule description or start datetime (e.g., 2025-10-01T09:00)")] string schedule,
        [Description("Duration in hours (number or text)")] string duration,
        [Description("ext_class_id")] string ext_class_id
    ) => new(ChatRole.User,
        $"Create a class named '{className}' for instructor '{instructorEmail}'. Schedule: '{schedule}'. Duration: '{duration}' hours, Ext class id '{ext_class_id}'. If needed, call the CreateClass tool with the appropriate payload.");

    [McpServerPrompt, Description("Remove a class by its ClassId3 (GUID).")]
    public static ChatMessage RemoveClass(
        [Description("The ClassId GUID of the class to remove")] string classId
    ) => new(ChatRole.User, $"Remove the class with ClassId {classId}. Use the RemoveClassByClassId tool.");

    [McpServerPrompt, Description("Get all classes information.")]
    public static ChatMessage GetAllClasses()
        => new(ChatRole.User, "Retrieve all classes. Use the GetAllClassInfo tool.");

    [McpServerPrompt, Description("Get a single class by ClassId (GUID).")]
    public static ChatMessage GetClassByClassId(
        [Description("ClassId GUID")] string classId
    ) => new(ChatRole.User, $"Get information for class with ClassId {classId}. Use the GetSingleClassInfoClassId tool.");

    // ===== Enrollments =====
    [McpServerPrompt, Description("Create an enrollment for a user in a class.")]
    public static ChatMessage EnrollUser(
        [Description("ClassId GUID to enroll into")] string classId,
        [Description("User's email address or identifier")] string userIdOrEmail,
        [Description("Enrollment role (e.g., student, instructor)")] string role
    ) => new(ChatRole.User,
        $"Enroll user '{userIdOrEmail}' into class {classId} as '{role}'. Use the CreateEnrollment tool with the right request body.");

    [McpServerPrompt, Description("Remove an enrollment from a class.")]
    public static ChatMessage UnenrollUser(
        [Description("ClassId GUID")] string classId,
        [Description("User's email or identifier to remove")] string userIdOrEmail
    ) => new(ChatRole.User, $"Remove enrollment for user '{userIdOrEmail}' from class {classId}. Use the RemoveEnrollment tool.");

    // ===== Schedules =====
    [McpServerPrompt, Description("Add a class date (schedule entry) to a class.")]
    public static ChatMessage AddClassDate(
        [Description("ClassId GUID")] string classId,
        [Description("Start ISO datetime, e.g., 2025-10-01T09:00")] string start,
        [Description("End ISO datetime, e.g., 2025-10-01T10:30")] string end
    ) => new(ChatRole.User,
        $"Add schedule entry for class {classId} from {start} to {end}. Use the AddClassDate tool.");

    [McpServerPrompt, Description("Remove a scheduled date by ScheduleId.")]
    public static ChatMessage RemoveClassDate(
        [Description("ScheduleId string")] string scheduleId
    ) => new(ChatRole.User, $"Remove class schedule with ScheduleId {scheduleId}. Use the RemoveSchedule tool.");

    // ===== Launch =====
    [McpServerPrompt, Description("Get a launch link for a user and class context.")]
    public static ChatMessage GetLaunchLink(
        [Description("ClassId GUID or external Id")] string classIdOrExtId,
        [Description("User email or external user Id")] string userIdOrEmail
    ) => new(ChatRole.User,
        $"Generate a launch URL for user '{userIdOrEmail}' and class '{classIdOrExtId}'. Use the GetLaunchUrl tool.");

    // ===== Reporting =====
    [McpServerPrompt, Description("Get attendance by internal ClassId (string).")]
    public static ChatMessage GetAttendanceByClassId(
        [Description("Internal ClassId (string)")] string classId
    ) => new(ChatRole.User, $"Get attendance for internal ClassId '{classId}'. Use the GetAttendanceByClassIdAsync tool.");

    [McpServerPrompt, Description("Get attendance by external ClassId (string).")]
    public static ChatMessage GetAttendanceByExtClassId(
        [Description("External ClassId (string)")] string extClassId
    ) => new(ChatRole.User, $"Get attendance for external ClassId '{extClassId}'. Use the GetAttendanceByExtClassIdAsync tool.");

    // ===== Users =====
    [McpServerPrompt, Description("Create a new user.")]
    public static ChatMessage CreateUserPrompt(
        [Description("User's email")] string email,
        [Description("User's first name")] string firstName,
        [Description("User's last name")] string lastName
    ) => new(ChatRole.User,
        $"Create a user with email {email}, first name '{firstName}', and last name '{lastName}'. Use the CreateUser tool.");

    [McpServerPrompt, Description("Update an existing user.")]
    public static ChatMessage UpdateUserPrompt(
        [Description("User identifier (email or external user Id)")] string userId,
        [Description("Fields to update (plain text)")] string updates
    ) => new(ChatRole.User, $"Update user '{userId}' with: {updates}. Use the UpdateUser tool.");

    [McpServerPrompt, Description("Remove a user by email.")]
    public static ChatMessage RemoveUserByEmail(
        [Description("User email")] string email
    ) => new(ChatRole.User, $"Remove the user with email {email}. Use the RemoveUserByEmailId tool.");

    // ===== Time Utilities =====
    [McpServerPrompt, Description("Convert a DateTime to Unix timestamp.")]
    public static ChatMessage ConvertToUnixTime(
        [Description("ISO 8601 datetime string (e.g., 2025-10-01T09:00:00)")] string dateTime
    ) => new(ChatRole.User, $"Convert '{dateTime}' to Unix timestamp in seconds. Use the ConvertToUnixTimeSeconds tool.");
}