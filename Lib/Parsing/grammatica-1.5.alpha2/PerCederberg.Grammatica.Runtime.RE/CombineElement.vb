' 
' * CombineElement.cs 
' * 
' * This library is free software; you can redistribute it and/or 
' * modify it under the terms of the GNU Lesser General Public License 
' * as published by the Free Software Foundation; either version 2.1 
' * of the License, or (at your option) any later version. 
' * 
' * This library is distributed in the hope that it will be useful, 
' * but WITHOUT ANY WARRANTY; without even the implied warranty of 
' * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
' * Lesser General Public License for more details. 
' * 
' * You should have received a copy of the GNU Lesser General Public 
' * License along with this library; if not, write to the Free 
' * Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, 
' * MA 02111-1307, USA. 
' * 
' * Copyright (c) 2003-2005 Per Cederberg. All rights reserved. 
' 

' Converted to VB.NET	[Eugene Ciloci; Nov 24, 2007]

Imports System.IO 

Imports Ciloci.Flee.PerCederberg.Grammatica.Runtime

Namespace PerCederberg.Grammatica.Runtime.RE 
    
    '* 
' * A regular expression combination element. This element matches 
' * two consecutive elements. 
' * 
' * @author Per Cederberg, <per at percederberg dot net> 
' * @version 1.5 
' 
    
    Friend Class CombineElement 
        Inherits Element 
        
        '* 
' * The first element. 
' 
        
        Private elem1 As Element 
        
        '* 
' * The second element. 
' 
        
        Private elem2 As Element 
        
        '* 
' * Creates a new combine element. 
' * 
' * @param first the first element 
' * @param second the second element 
' 
        
        Public Sub New(ByVal first As Element, ByVal second As Element) 
            elem1 = first 
            elem2 = second 
        End Sub 
        
        '* 
' * Creates a copy of this element. The copy will be an 
' * instance of the same class matching the same strings. 
' * Copies of elements are necessary to allow elements to cache 
' * intermediate results while matching strings without 
' * interfering with other threads. 
' * 
' * @return a copy of this element 
' 
        
        Public Overloads Overrides Function Clone() As Object 
            Return New CombineElement(elem1, elem2) 
        End Function 
        
        '* 
' * Returns the length of a matching string starting at the 
' * specified position. The number of matches to skip can also be 
' * specified, but numbers higher than zero (0) cause a failed 
' * match for any element that doesn't attempt to combine other 
' * elements. 
' * 
' * @param m the matcher being used 
' * @param input the input character stream to match 
' * @param start the starting position 
' * @param skip the number of matches to skip 
' * 
' * @return the length of the longest matching string, or 
' * -1 if no match was found 
' * 
' * @throws IOException if an I/O error occurred 
' 
        
        Public Overloads Overrides Function Match(ByVal m As Matcher, ByVal input As LookAheadReader, ByVal start As Integer, ByVal skip As Integer) As Integer 
            
            Dim length1 As Integer = -1 
            Dim length2 As Integer = 0 
            Dim skip1 As Integer = 0 
            Dim skip2 As Integer = 0 
            
            While skip >= 0 
                length1 = elem1.Match(m, input, start, skip1) 
                If length1 < 0 Then 
                    Return -1 
                End If 
                length2 = elem2.Match(m, input, start + length1, skip2) 
                If length2 < 0 Then 
                    skip1 += 1 
                    skip2 = 0 
                Else 
                    skip2 += 1 
                    skip -= 1 
                End If 
            End While 
            
            Return length1 + length2 
        End Function 
        
        '* 
' * Prints this element to the specified output stream. 
' * 
' * @param output the output stream to use 
' * @param indent the current indentation 
' 
        
        Public Overloads Overrides Sub PrintTo(ByVal output As TextWriter, ByVal indent As String) 
            elem1.PrintTo(output, indent) 
            elem2.PrintTo(output, indent) 
        End Sub 
    End Class 
End Namespace 