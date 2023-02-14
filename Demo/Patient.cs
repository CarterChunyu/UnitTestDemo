using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;


namespace Demo
{
    public class Patient:Person,INotifyPropertyChanged
    {
        public Patient()
        {
            IsNew = true;
            BloodSurgar = 4.900003f;
            History = new List<string>();
        }

        public string FullName => $"{FirstName} {LastName}";
        public int HeartBeatRun { get; set; }
        public bool IsNew { get; set; }
        public float BloodSurgar { get; set; }

        public List<string> History;

        public event EventHandler<EventArgs> PatientSlept;        

        public void OnPatientSleep()
        {
            PatientSlept?.Invoke(sender:this,e:EventArgs.Empty);
        }
        public void Sleep()
        {
            OnPatientSleep();
        }
        public void IncreaseHeartBeatRate()
        {
            HeartBeatRun = CalCulateHeartBeatRate() + 2;
            OnPropertyChanged(nameof(HeartBeatRun));
        }
        private int CalCulateHeartBeatRate()
        {
            var random = new Random();
            return random.Next(1, 100);
        }
        public void NotAllowed()
        {
            throw new InvalidOperationException(message: "Not able to create");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(sender: this, e: new PropertyChangedEventArgs(propertyName));
        }
    }
}
