using Coso.ClassComNet.Sdk;
using Coso.ClassComNet.Sdk.Models;

namespace ClassApiMCPServer.Services;

public class ClassApiClient(IClassComClient classComClient)
{
    #region Class

    public async Task<CreateClassResponse> CreateClass(CreateClassRequest request)
    {
        request.Settings = new Settings() {JbhTime = 0, WaitingRoom = false, JoinBeforeHost = true};
        var result = await classComClient.Classes.CreateAsync(request);
        return result;
    }

    public async Task<IEnumerable<Class>> GetAllClassInfo()
    {
        var result = await classComClient.Classes.GetAllAsync();
        return result;
    }

    public async Task<Class> GetSingleClassInfoClassId(Guid classId)
    {
        var result = await classComClient.Classes.GetByClassIdAsync(classId);
        return result;
    }

    public async Task<bool> RemoveClassByClassId(Guid classId)
    {
        var result = await classComClient.Classes.RemoveByClassId(classId);
        return true;
    }

    #endregion Class

    #region Enrollments

    public async Task<Enrollment> CreateEnrollment(CreateEnrollmentRequest request)
    {
        var result = await classComClient.Enrollments.CreateAsync(request);
        return result;
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByClassId(Guid classId)
    {
        var result = await classComClient.Enrollments.GetByClassIdAsync(classId);
        return result;
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByExtClassId(string extClassId)
    {
        var result = await classComClient.Enrollments.GetByExtClassIdAsync(extClassId);
        return result;
    }

    public async Task<Enrollment> UpdateEnrollment(UpdateEnrollmentRequest request)
    {
        var result = await classComClient.Enrollments.UpdateAsync(request);
        return result;
    }

    public async Task<bool> RemoveEnrollment(DeleteEnrollmentRequest request)
    {
        var result = await classComClient.Enrollments.RemoveAsync(request);
        return result;
    }

    #endregion Enrollments

    #region Launch

    public async Task<LaunchUrlResponse> GetLaunchUrl(GetLaunchLinkRequest request)
    {
        var result = await classComClient.LaunchUrl.GetLaunchUrl(request);
        return result;
    }

    #endregion Launch

    #region Reporting

    public async Task<GetAttendanceResponse> GetAttendanceByClassIdAsync(string classId)
    {
        var result = await classComClient.Reporting.GetAttendanceByClassIdAsync(classId);
        return result;
    }

    public async Task<GetAttendanceResponse> GetAttendanceByExtClassIdAsync(string extClassId)
    {
        var result = await classComClient.Reporting.GetAttendanceByExtClassIdAsync(extClassId);
        return result;
    }

    #endregion Reporting

    #region Schedules

    public async Task<CreateScheduleResponse> AddClassDate(AddClassDateRequest request)
    {
        var result = await classComClient.Schedules.AddClassDate(request);
        return result;
    }

    public async Task<IEnumerable<Schedule>> GetClassDatesByClassId(Guid classId)
    {
        var result = await classComClient.Schedules.GetClassDatesByClassId(classId);
        return result;
    }

    public async Task<bool> RemoveSchedule(string scheduleId)
    {
        var result = await classComClient.Schedules.Remove(scheduleId);
        return result;
    }

    #endregion Schedules

    #region Users

    public async Task<User> GetById(int id)
    {
        var result = await classComClient.Users.GetByIdAsync(id);
        return result;
    }

    public async Task<User> GetByExtUserId(string extUserId)
    {
        var result = await classComClient.Users.GetByExtUserIdAsync(extUserId);
        return result;
    }

    public async Task<User> GetByEmailId(string email)
    {
        var result = await classComClient.Users.GetByEmailIdAsync(email);
        return result;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var result = await classComClient.Users.GetAllAsync();
        return result;
    }

    public async Task<User> CreateUser(CreateUserRequest request)
    {
        var result = await classComClient.Users.CreateAsync(request);
        return result;
    }

    public async Task<User> UpdateUser(UpdateUserRequest request)
    {
        var result = await classComClient.Users.UpdateAsync(request);
        return result;
    }

    public async Task<bool> RemoveUserById(int id)
    {
        var result = await classComClient.Users.RemoveByIdAsync(id);
        return result;
    }

    public async Task<bool> RemoveUserByExtUserId(string extUserId)
    {
        var result = await classComClient.Users.RemoveByExtUserIdAsync(extUserId);
        return result;
    }

    public async Task<bool> RemoveUserByEmailId(string email)
    {
        var result = await classComClient.Users.RemoveByEmailIdAsync(email);
        return result;
    }

    #endregion
}