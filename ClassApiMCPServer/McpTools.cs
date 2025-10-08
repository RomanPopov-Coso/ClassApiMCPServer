using System;
using System.Collections.Generic;
using System.ComponentModel;
using ClassApiMCPServer.Services;
using Coso.ClassComNet.Sdk;
using Coso.ClassComNet.Sdk.Models;
using ModelContextProtocol.Server;

namespace ClassApiMCPServer;

[McpServerToolType]
public static class McpTools
{
    // ===== Classes =====
    [McpServerTool, Description("Create a new class. Provide instructor, class, and optional schedule info. Returns the created class object.")]
    public static Task<CreateClassResponse> CreateClass(CreateClassRequest request, ClassApiClient classComClient)
    {
        return classComClient.CreateClass(request);
    }

    [McpServerTool, Description("Get all classes.")]
    public static Task<IEnumerable<Class>> GetAllClassInfo(ClassApiClient classComClient)
    {
        return classComClient.GetAllClassInfo();
    }

    [McpServerTool, Description("Get a single class by its ClassId (GUID).")]
    public static Task<Class> GetSingleClassInfoClassId(Guid classId, ClassApiClient classComClient)
    {
        return classComClient.GetSingleClassInfoClassId(classId);
    }

    [McpServerTool, Description("Remove a class by its ClassId (GUID). Returns true if the class was removed.")]
    public static Task<bool> RemoveClassByClassId(Guid classId, ClassApiClient classComClient)
    {
        return classComClient.RemoveClassByClassId(classId);
    }

    // ===== Enrollments =====
    [McpServerTool, Description("Create an enrollment for a user in a class.")]
    public static Task<Enrollment> CreateEnrollment(CreateEnrollmentRequest request, ClassApiClient classComClient)
    {
        return classComClient.CreateEnrollment(request);
    }

    [McpServerTool, Description("Get all enrollments by ClassId (GUID).")]
    public static Task<IEnumerable<Enrollment>> GetEnrollmentsByClassId(Guid classId, ClassApiClient classComClient)
    {
        return classComClient.GetEnrollmentsByClassId(classId);
    }

    [McpServerTool, Description("Get all enrollments by external ClassId.")]
    public static Task<IEnumerable<Enrollment>> GetEnrollmentsByExtClassId(string extClassId, ClassApiClient classComClient)
    {
        return classComClient.GetEnrollmentsByExtClassId(extClassId);
    }

    [McpServerTool, Description("Update an enrollment.")]
    public static Task<Enrollment> UpdateEnrollment(UpdateEnrollmentRequest request, ClassApiClient classComClient)
    {
        return classComClient.UpdateEnrollment(request);
    }

    [McpServerTool, Description("Remove an enrollment.")]
    public static Task<bool> RemoveEnrollment(DeleteEnrollmentRequest request, ClassApiClient classComClient)
    {
        return classComClient.RemoveEnrollment(request);
    }

    // ===== Launch =====
    [McpServerTool, Description("Get a launch URL for a user/class context.")]
    public static Task<LaunchUrlResponse> GetLaunchUrl(GetLaunchLinkRequest request, ClassApiClient classComClient)
    {
        return classComClient.GetLaunchUrl(request);
    }

    // ===== Reporting =====
    [McpServerTool, Description("Get attendance by internal ClassId (string).")]
    public static Task<GetAttendanceResponse> GetAttendanceByClassIdAsync(string classId, ClassApiClient classComClient)
    {
        return classComClient.GetAttendanceByClassIdAsync(classId);
    }

    [McpServerTool, Description("Get attendance by external ClassId (string).")]
    public static Task<GetAttendanceResponse> GetAttendanceByExtClassIdAsync(string extClassId, ClassApiClient classComClient)
    {
        return classComClient.GetAttendanceByExtClassIdAsync(extClassId);
    }

    // ===== Schedules =====
    [McpServerTool, Description("Add a class date (schedule entry) to a class.")]
    public static Task<CreateScheduleResponse> AddClassDate(AddClassDateRequest request, ClassApiClient classComClient)
    {
        return classComClient.AddClassDate(request);
    }

    [McpServerTool, Description("Get all scheduled dates for a class by ClassId (GUID).")]
    public static async Task<IEnumerable<Schedule>> GetClassDatesByClassId(Guid classId, ClassApiClient classComClient)
    {
        var result = await classComClient.GetClassDatesByClassId(classId);
        return result;
    }

    [McpServerTool, Description("Remove a schedule entry by its ScheduleId. Returns true if removed.")]
    public static Task<bool> RemoveSchedule(string scheduleId, ClassApiClient classComClient)
    {
        return classComClient.RemoveSchedule(scheduleId);
    }

    // ===== Users =====
    [McpServerTool, Description("Get a user by internal numeric Id.")]
    public static Task<User> GetById(int id, ClassApiClient classComClient)
    {
        return classComClient.GetById(id);
    }

    [McpServerTool, Description("Get a user by external user Id.")]
    public static Task<User> GetByExtUserId(string extUserId, ClassApiClient classComClient)
    {
        return classComClient.GetByExtUserId(extUserId);
    }

    [McpServerTool, Description("Get a user by email address.")]
    public static Task<User> GetByEmailId(string email, ClassApiClient classComClient)
    {
        return classComClient.GetByEmailId(email);
    }

    [McpServerTool, Description("Get all users.")]
    public static Task<IEnumerable<User>> GetAllUsers(ClassApiClient classComClient)
    {
        return classComClient.GetAllUsers();
    }

    [McpServerTool, Description("Create a new user.")]
    public static Task<User> CreateUser(CreateUserRequest request, ClassApiClient classComClient)
    {
        return classComClient.CreateUser(request);
    }

    [McpServerTool, Description("Update a user.")]
    public static Task<User> UpdateUser(UpdateUserRequest request, ClassApiClient classComClient)
    {
        return classComClient.UpdateUser(request);
    }

    [McpServerTool, Description("Remove a user by internal numeric Id. Returns true if removed.")]
    public static Task<bool> RemoveUserById(int id, ClassApiClient classComClient)
    {
        return classComClient.RemoveUserById(id);
    }

    [McpServerTool, Description("Remove a user by external user Id. Returns true if removed.")]
    public static Task<bool> RemoveUserByExtUserId(string extUserId, ClassApiClient classComClient)
    {
        return classComClient.RemoveUserByExtUserId(extUserId);
    }

    [McpServerTool, Description("Remove a user by email address. Returns true if removed.")]
    public static Task<bool> RemoveUserByEmailId(string email, ClassApiClient classComClient)
    {
        return classComClient.RemoveUserByEmailId(email);
    }

    // ===== Time Utilities =====
    [McpServerTool, Description("Convert a DateTime to Unix timestamp in seconds. If DateTime kind is Unspecified, it is treated as UTC.")]
    public static long ConvertToUnixTimeSeconds(DateTime dateTime, TimeService timeService)
    {
        return timeService.ConvertToUnixTimeSeconds(dateTime);
    }
}