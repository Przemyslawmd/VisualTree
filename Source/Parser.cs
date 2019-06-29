
using System.Collections.Generic;
using System;

namespace VisualTree
{
    class Parser
    {
        public List< int > GetNodesValues( String text, out Result result )
        {
            if ( text.Length == 0 )
            {
                result = Result.NO_DATA_TO_CREATE_TREE;
                return null;
            }
            
            return ParseText( text, out result );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private List< int > ParseText( String text, out Result result )
        {
            foreach( var token in text )
            {
                if ( Char.IsDigit( token ) == false && token.Equals( ' ' ) == false && token.Equals( ',' ) == false )
                {
                    result = Result.IMPROPER_DATA;
                    return null;
                }
            }
        
            List< int > nodesValues = new List< int >();
            int number = 0;
            bool lastTokenIsDigit = false;

            foreach( var token in text )
            {
                if ( Char.IsDigit( token ))
                {
                    number *= 10;
                    number += ( int ) Char.GetNumericValue( token );
                    lastTokenIsDigit = true;
                    continue;
                }
                        
                if ( lastTokenIsDigit )
                {
                    if ( CheckLastNumber() is false )
                    {
                        result = Result.DUPLICATED_SYMBOL;
                        return null;
                    }
                }

                number = 0;
                lastTokenIsDigit = false;		
            }
            
            if ( CheckLastNumber() is false )
            {
                result = Result.DUPLICATED_SYMBOL;
                return null;
            }

            if ( nodesValues.Count == 0 )
            {
                result = Result.IMPROPER_DATA;
                return null;
            }

            result = Result.OK;
            return nodesValues;


            bool CheckLastNumber()
            {
                if ( nodesValues.Contains( number ) is false )
                {
                    nodesValues.Add( number );
                }
                else if ( Settings.RemoveDuplicatedNodes is false )
                {
                    return false;
                }
                return true;
            }
        }
    }
}

