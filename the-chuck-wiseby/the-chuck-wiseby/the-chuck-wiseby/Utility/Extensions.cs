using System;
using the_chuck_wiseby.Models;

namespace the_chuck_wiseby.Utility
{
    public static class Extensions
    {
        public static ChuckMessage CastToChuckMessage(this object o)
        {
            try
            {
                return o as ChuckMessage;
            }
            catch (System.Exception ex)
            {
                throw new InvalidCastException(ex.Message);
            }
        }
    }
}
