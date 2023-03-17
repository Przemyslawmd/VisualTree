﻿
using System.Collections.Generic;
using System;

namespace VisualTree
{
    class Parser
    {
        public List< int > GetKeys( String text, out Result result )
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
                if ( Char.IsDigit( token ) is false && token.Equals( ' ' ) is false && token.Equals( ',' ) is false )
                {
                    result = Result.IMPROPER_DATA;
                    return null;
                }
            }
        
            var nodesValues = new List< int >();
            int number = 0;
            bool lastTokenIsDigit = false;

            foreach( var token in text )
            {
                if ( Char.IsDigit( token ))
                {
                    number = number * 10 + ( int ) Char.GetNumericValue( token );
                    lastTokenIsDigit = true;
                    continue;
                }
                        
                if ( lastTokenIsDigit && CheckLastNumber() is false )
                {
                    result = Result.DUPLICATED_SYMBOL;
                    return null;
                }

                number = 0;
                lastTokenIsDigit = false;		
            }
            
            if ( lastTokenIsDigit && CheckLastNumber() is false )
            {
                result = Result.DUPLICATED_SYMBOL;
            }
            else if ( nodesValues.Count == 0 )
            {
                result = Result.IMPROPER_DATA;
            }
            else
            { 
                result = Result.OK;
            }
            
            return result == Result.OK ? nodesValues : null;


            bool CheckLastNumber()
            {
                if ( nodesValues.Contains( number ) is false )
                {
                    nodesValues.Add( number );
                    return true;
                }                
                return Settings.RemoveDuplicatedNodes;
            }
        }
    }
}

