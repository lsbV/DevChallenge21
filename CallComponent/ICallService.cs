using Core;

namespace CallComponent;

public interface ICallService
{
    Task<CallId> ProcessCallAsync(Audio audio);
    Task<Call> GetCallByIdAsync(CallId id);
}