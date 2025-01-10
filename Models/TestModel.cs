using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace PlotDemo.Models
{

    public class TestModel : BindingBase
    {
        private double timeStamp;

        public string Name { get; set; }
        public string Value { get; set; }
        /// <summary>
        /// ms
        /// </summary>
        public double TimeStamp { get => timeStamp; set => SetProperty(ref timeStamp, value); }

        public override string ToString()
        {
            return Name;
        }
    }

    public abstract class BindingBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public SynchronizationContext UIContext;
        protected virtual bool SetProperty<T>(ref T storage, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (UIContext != null)
            {
                UIContext.Post(_ =>
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
                }, null);

            }
            else
            {
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            try
            {
                //PropertyChanged?.BeginInvoke(this,args,null,null);
                this.PropertyChanged?.Invoke(this, args);
            }
            catch (System.Exception)
            {
            }
        }

    }

}
