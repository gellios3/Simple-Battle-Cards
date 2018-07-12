using strange.extensions.mediation.impl;

namespace View
{
    public class TargetMediator<T> : EventMediator
        where T : EventView
    {
        [Inject]
        public T View { get; set; }
    }
}