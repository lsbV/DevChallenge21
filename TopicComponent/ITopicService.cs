using Core;

namespace TopicComponent;

public interface ITopicService
{
    public Task<IEnumerable<Topic>> GetCategoriesAsync();
    public Task<Topic> GetTopicAsync(TopicId topicId);
    public Task<Topic> CreateTopicAsync(Topic topic);
    public Task<Topic> UpdateTopicAsync(Topic topic);
    public Task<Topic> DeleteTopicAsync(TopicId topicId);
}