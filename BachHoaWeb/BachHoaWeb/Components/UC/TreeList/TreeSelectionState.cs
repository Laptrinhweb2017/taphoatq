using Microsoft.AspNetCore.Components;

namespace MyControl
{
    public class TreeSelectionState
    {
        public object? SelectedItem { get; set; }

        public EventCallback<object> OnSelectionChanged { get; set; }
        public event Action? SelectionUpdated;

        public async Task SelectAsync(object? item)
        {
            if (!Equals(SelectedItem, item))
            {
                SelectedItem = item;
                SelectionUpdated?.Invoke();

                if (OnSelectionChanged.HasDelegate)
                    await OnSelectionChanged.InvokeAsync(item);
            }
        }

    }
}
