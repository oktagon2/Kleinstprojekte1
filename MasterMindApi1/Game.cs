using System;
using System.Collections.Generic;

namespace MasterMindApi1
{
    public class Game
    {
        public List<Code> PossibleCodes { get; set; }
        public List<Code> RatedCodes { get; set; }

        private Random random = new Random();
        public Code masterCode = null;
        public Code tryCode = null;

        public void NewGame( bool computerGuesses= false)
        {
            PossibleCodes = new List<Code>();
            RatedCodes = new List<Code>();
            for( int color1= 1; color1<= 8; color1++)
            {
                List<int> usedColors = new List<int>();
                usedColors.Add(color1);
                for (int color2 = 1; color2 <= 8; color2++)
                {
                    if( !usedColors.Contains( color2))
                    {
                        usedColors.Add(color2);
                        for (int color3 = 1; color3 <= 8; color3++)
                        {
                            if (!usedColors.Contains(color3))
                            {
                                usedColors.Add(color3);
                                for (int color4 = 1; color4 <= 8; color4++)
                                {
                                    if (!usedColors.Contains(color4))
                                    {
                                        usedColors.Add(color4);
                                        PossibleCodes.Add(new Code(color1, color2, color3, color4));
                                        usedColors.Remove(color4);
                                    }
                                }
                                usedColors.Remove(color3);
                            }
                        }
                        usedColors.Remove(color2);
                    }
                }
                usedColors.Remove(color1);
            }
            masterCode = GetRandomCode();
        }

        public void RateLastTryWith(Rating rating)
        {
            List<Code> codesToDelete = new List<Code>();
            foreach( Code possibleCode in PossibleCodes)
            {
                if( rating!= new Rating( possibleCode, tryCode))
                {
                    codesToDelete.Add(possibleCode);
                }
            }
            foreach( Code codeToDelete in codesToDelete)
            {
                PossibleCodes.Remove(codeToDelete);
            }
            if( PossibleCodes.Count== 0)
            {
                throw new Exception("PossibleCodes.Count== 0");
            }
        }

        public Rating EvaluateCode( Code code)
        {
            return new Rating(masterCode, code);
        }

        public int GetRandomValue( int maxValue)
        {
            return random.Next(maxValue);
        }

        public Code GetRandomCode()
        {
            Code ret= PossibleCodes[GetRandomValue(PossibleCodes.Count)];
            return ret;
        }

        public Code GetNextTry()
        {
            Code ret = GetRandomCode();
            tryCode = ret;
            return ret;
        }
    }
}
