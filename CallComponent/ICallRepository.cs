using Core;

namespace CallComponent;

public interface ICallRepository
{
    Task<Call> GetCallByIdAsync(CallId id);
    Task<CallId> SaveCallAsync(Call call);
    Task<CallId> UpdateCallAsync(Call call);
}