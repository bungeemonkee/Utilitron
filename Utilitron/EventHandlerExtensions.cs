using System;

namespace Utilitron
{
    /// <summary>
    /// Extensions for any event handlers.
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// De-register any handlers attached to this event.s
        /// </summary>
        /// <typeparam name="T">The event argument type for the event.</typeparam>
        /// <param name="eventHandler">The event to de-register all handlers for.</param>
        public static void Deregister<T>(this EventHandler<T> eventHandler)
        {
            if (eventHandler == null)
            {
                return;
            }

            foreach (var func in eventHandler.GetInvocationList())
            {
                eventHandler -= (EventHandler<T>)func;
            }
        }
    }
}
