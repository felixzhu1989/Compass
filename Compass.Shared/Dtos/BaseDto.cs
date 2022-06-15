using System.ComponentModel;
using System.Runtime.CompilerServices;
using Compass.Shared.Annotations;

namespace Compass.Shared.Dtos;

public class BaseDto : INotifyPropertyChanged
{
    public int Id { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
