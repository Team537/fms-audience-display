using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team537.Audience.Display.DataModel;

namespace Team537.Audience.Display.ViewModel
{
    public delegate void PlaySoundEventHandler(object sender, PlaySoundEventArgs e);

    public class PlaySoundEventArgs : EventArgs
    {
        public SoundEffect SoundEffect { get; set; }
    }

    public class MainWindowViewModel : BindableObject
    {
        public event PlaySoundEventHandler PlaySoundHandler;

        public void PlaySound(SoundEffect sound)
        {
            if (PlaySoundHandler != null)
            {
                var eventArgs = new PlaySoundEventArgs() { SoundEffect = sound };
                PlaySoundHandler(this, eventArgs);
            }
        }

        private int matchNumber;
        private int totalTime;
        private int timeLeft;

        private int red1;
        private int red2;
        private int red3;
        private int redScore;

        private int blue1;
        private int blue2;
        private int blue3;
        private int blueScore;

        public int MatchNumber
        {
            get { return matchNumber; }
            set
            {
                if (value != matchNumber)
                {
                    matchNumber = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int TotalTime
        {
            get { return totalTime; }
            set
            {
                if (value != totalTime)
                {
                    totalTime = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("TimeValue");
                }
            }
        }

        public int TimeLeft
        {
            get { return timeLeft; }
            set
            {
                if (value != timeLeft)
                {
                    timeLeft = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("TimeValue");
                }
            }
        }

        public int TimeValue
        {
            get { return totalTime - timeLeft; }
        }

        public int Red1
        {
            get { return red1; }
            set
            {
                if (value != red1)
                {
                    red1 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Red2
        {
            get { return red2; }
            set
            {
                if (value != red2)
                {
                    red2 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Red3
        {
            get { return red3; }
            set
            {
                if (value != red3)
                {
                    red3 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int RedScore
        {
            get { return redScore; }
            set
            {
                if (value != redScore)
                {
                    redScore = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Blue1
        {
            get { return blue1; }
            set
            {
                if (value != blue1)
                {
                    blue1 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Blue2
        {
            get { return blue2; }
            set
            {
                if (value != blue2)
                {
                    blue2 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Blue3
        {
            get { return blue3; }
            set
            {
                if (value != blue3)
                {
                    blue3 = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int BlueScore
        {
            get { return blueScore; }
            set
            {
                if (value != blueScore)
                {
                    blueScore = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
    }
}
