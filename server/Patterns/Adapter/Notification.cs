/**
 * The Target interface represents the interface that your application's classes
 * already follow.
 */
namespace BoomermanServer.Patterns.Adapter
{
    public interface Notification
    {
        void Send(string title, string message);
    }
}