Imports System
Imports gx

' ------------------------------------------------------------------------------
' Example tree:
'
'   <default>
'       <mymodule>
'           <testprop1 value="1"/>
'           <testprop2 value="2"/>
'           <testprop3 value="3"/>
'       </mymodule>
'   </default>
'   <pargroup>
'       <mymodule>
'           <testprop2 value="22"/>
'       </mymodule>
'   </pargroup>
'   <mygroup parent="/pargroup"/>
'       <mymodule>
'           <testprop1 value="111"/>
'       </mymodule>
'   </mygroup>
'
' ------------------------------------------------------------------------------


Module Main
	Sub Main()
		Try
			' Creates the properties class
			Dim prop As gxProperty = new gxProperty()
			prop.SetProperty("/default/mymodule/testprop1", 1)
			prop.SetProperty("/default/mymodule/testprop2", 2)
			prop.SetProperty("/default/mymodule/testprop3", 3)

			' Creates the pargroup with testprop2
			prop.SetProperty("/pargroup/mymodule/testprop2", 22)
	
			' Creates the mygroup with testprop1
			prop.SetProperty("/mygroup/mymodule/testprop1", 111)

			' Updates parent for the mygroup
			prop.SetPropertyParent("/mygroup", "/pargroup")
		
			' Reads properties
			Dim ivalue1 As integer = prop.GetPropertyInt("/mygroup/mymodule/testprop1")
			Dim ivalue2 As integer = prop.GetPropertyInt("/mygroup/mymodule/testprop2")
			Dim ivalue3 As integer = prop.GetPropertyInt("/mygroup/mymodule/testprop3")
		
			' Reads type of properties
			Dim type1 As integer = prop.GetPropertyType("/mygroup/mymodule/testprop1")
			Dim type2 As integer = prop.GetPropertyType("/mygroup/mymodule/testprop2")
			Dim type3 As integer = prop.GetPropertyType("/mygroup/mymodule/testprop3")
	
			' Description of property types			
			Dim property_types() As string = { _
				"INVALID",		_
				"NOVALUE",		_
				"DIRECT ",		_
				"INHERIT",		_
				"DEFAULT"		_
			}
		
			' Displays properties
			Console.WriteLine("testprop1={0}, type={1}:{2} (from /mygroup/mymodule/testprop1)", ivalue1, type1, property_types.GetValue(type1))
			Console.WriteLine("testprop2={0}, type={1}:{2} (from /mygroup/mymodule/testprop2)", ivalue2, type2, property_types.GetValue(type2))
			Console.WriteLine("testprop3={0}, type={1}:{2} (from /mygroup/mymodule/testprop3)", ivalue3, type3, property_types.GetValue(type3))
					
			' Removes all created properties
			prop.RmProperties("/default/mymodule")
			prop.RmProperties("/pargroup")
			prop.RmProperties("/mygroup")
				
	
			Catch ex As exception:
				Console.WriteLine("An error occurred: {0}", ex.Message)
		End Try
	End Sub
End Module
