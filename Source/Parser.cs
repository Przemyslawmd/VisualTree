
using System.Collections.Generic;
using System;

namespace VisualTree
{
    class Parser
    {
        public List< int > GetNodesValues( String text, ref Result result )
        {
            if ( text.Length == 0 )
            {
                result = Result.NO_DATA_TO_CREATE_TREE;
                return null;
            }
            
            return ParseText( text, ref result );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private List< int > ParseText( String text, ref Result result )
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
                    if ( nodesValues.Contains( number ))
                    {
                        result = Result.DUPLICATED_SYMBOL;
                        return null;
                    }
                    
                    nodesValues.Add( number );
                }

                number = 0;
                lastTokenIsDigit = false;		
            }
            
            if ( lastTokenIsDigit )
            {
                if ( nodesValues.Contains( number ))
                {
                    result = Result.DUPLICATED_SYMBOL;
                    return null;
                }
                
                nodesValues.Add( number );
            }
            
            if ( nodesValues.Count == 0 )
            {
                result = Result.IMPROPER_DATA;
                return null;
            }

            return nodesValues;
        }
    }
}

