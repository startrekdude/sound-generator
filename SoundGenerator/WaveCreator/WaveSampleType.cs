using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoundGenerator.WaveCreator
{
  public enum WaveSampleType : uint
   {
       Normal = 44100,
       LowQuality = 11025,
       Small = 22050,
       CDXA = 37800,
       Professional = 48000,
       Huge = 88200
   }
}
