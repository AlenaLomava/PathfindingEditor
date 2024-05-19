using System;

namespace Assets.Scripts
{
    public interface ISelectableController
    {
        ISelectable CurrentClicked { get; }

        ISelectable PreviousClicked { get; }

        event Action<ISelectable> OnClicked;
    }
}
