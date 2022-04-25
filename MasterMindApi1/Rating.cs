using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMindApi1
{
    public class Rating
    {
        public int Reds { get; set; }
        public int Whites { get; set; }
        public bool Ok { get; set; }

        public bool Check()
        {
            bool ret = true;
            ret &= (Reds >= 0) && (Reds <= 4);
            ret &= (Whites >= 0) && (Whites <= 4);
            ret &= (Reds + Whites) <= 4;
            Ok = ret;
            return ret;
        }

        public Rating( string rating)
        {
            Ok = false;
            if( rating.Length== 2)
            {
                Reds = Convert.ToInt32(rating[0]) - 48;
                Whites = Convert.ToInt32(rating[1]) - 48;
                Check();
            }
        }

        public Rating( Code sollCode, Code istCode)
        {
            Reds = 0;
            Whites = 0;
            List<bool> sollRates = new List<bool>(new bool[] { false, false, false, false });
            List<bool> istRates= new List<bool>(new bool[] { false, false, false, false });

            // Red rating
            for ( int pos= 0; pos< 4; pos++)
            {
                if( sollCode.Colors[pos]== istCode.Colors[pos])
                {
                    Reds++;
                    sollRates[pos] = true;
                    istRates[pos] = true;
                }
            }
            for( int pos= 0; pos< 4; pos++)
            {
                if( !sollRates[pos])
                {
                    for( int pos2= 0; pos2< 4; pos2++)
                    {
                        if( pos!= pos2)
                        {
                            if (!istRates[pos2])
                            {
                                if(sollCode.Colors[pos] == istCode.Colors[pos2])
                                {
                                    Whites++;
                                    sollRates[pos] = true;
                                    istRates[pos2] = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool Equals( Rating rating)
        {
            if( rating is null)
            {
                return false;
            }

            if( Object.ReferenceEquals( this, rating))
            {
                return true;
            }

            if( this.GetType()!= rating.GetType())
            {
                return false;
            }

            return (this.Whites == rating.Whites)
                && (this.Reds == rating.Reds);
        }

        public override bool Equals(object obj) => this.Equals(obj as Rating);

        public override int GetHashCode() => (Reds, Whites).GetHashCode();

        public static bool operator ==(Rating lhs, Rating rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Rating lhs, Rating rhs) => !(lhs == rhs);

    }
}
