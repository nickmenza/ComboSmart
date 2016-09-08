@echo off
if "%1"=="" (
	call :generate %*
) else (
	call :%*
)
goto :EOF

rem --- Generate ---------------------------------------------------------------
:generate

echo Generating project files for Visual Studio 8 VB.NET ...

mkdir vb80

set PROJECTS=

for /D %%d in (gx*) do (
	for %%i in ( %%d\*.vb ) do (
		call :add %%~ni
		mkdir vb80\%%~ni
		call :vbproj %%d %%~ni > vb80\%%~ni\%%~ni.vbproj
		mkdir "vb80\%%~ni\My Project"
		call :Application_myapp > "vb80\%%~ni\My Project\Application.myapp
		call :Application_Designer_vb > "vb80\%%~ni\My Project\Application.Designer.vb
		call :AssemblyInfo_vb %%~ni > "vb80\%%~ni\My Project\AssemblyInfo.vb
		call :Resources_resx > "vb80\%%~ni\My Project\Resources.resx
		call :Resources_Designer_vb %%~ni > "vb80\%%~ni\My Project\Resources.Designer.vb
		call :Settings_settings > "vb80\%%~ni\My Project\Settings.settings
		call :Settings_Designer_vb %%~ni > "vb80\%%~ni\My Project\Settings.Designer.vb
	)
)

call :sln > vb80.sln 

goto :EOF

rem --- Projects list ----------------------------------------------------------
:add

set PROJECTS=%PROJECTS%^ %*

goto :EOF

rem --- Project ----------------------------------------------------------------

:vbproj

echo ﻿^<?xml version="1.0" encoding="utf-8"?^>
echo ^<Project DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003"^>
echo   ^<PropertyGroup^>
echo     ^<Configuration Condition=" '$(Configuration)' == '' "^>Debug^</Configuration^>
echo     ^<Platform Condition=" '$(Platform)' == '' "^>x64^</Platform^>
echo     ^<ProductVersion^>8.0.50727^</ProductVersion^>
echo     ^<SchemaVersion^>2.0^</SchemaVersion^>
echo     ^<ProjectGuid^>{DB24D70A-6E03-431f-AB73-3075CF7A2E3A}^</ProjectGuid^>
echo     ^<OutputType^>Exe^</OutputType^>
echo     ^<StartupObject^>%2.Main^</StartupObject^>
echo     ^<RootNamespace^>%2^</RootNamespace^>
echo     ^<AssemblyName^>%2^</AssemblyName^>
echo     ^<MyType^>Console^</MyType^>
echo   ^</PropertyGroup^>
echo   ^<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|x64' "^>
echo       ^<DebugSymbols^>true^</DebugSymbols^>
echo       ^<DefineDebug^>true^</DefineDebug^>
echo       ^<DefineTrace^>true^</DefineTrace^>
echo       ^<OutputPath^>bin\x64\Debug\^</OutputPath^>
echo       ^<DocumentationFile^>%2.xml^</DocumentationFile^>
echo       ^<NoWarn^>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022^</NoWarn^>
echo       ^<DebugType^>full^</DebugType^>
echo       ^<PlatformTarget^>x64^</PlatformTarget^>
echo     ^</PropertyGroup^>
echo     ^<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|x64' "^>
echo       ^<DefineTrace^>true^</DefineTrace^>
echo       ^<OutputPath^>bin\x64\Release\^</OutputPath^>
echo       ^<DocumentationFile^>%2.xml^</DocumentationFile^>
echo       ^<Optimize^>true^</Optimize^>
echo       ^<NoWarn^>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022^</NoWarn^>
echo       ^<DebugType^>pdbonly^</DebugType^>
echo       ^<PlatformTarget^>x64^</PlatformTarget^>
echo     ^</PropertyGroup^>
echo   ^<ItemGroup^>
echo     ^<Reference Include="System" /^>
echo     ^<Reference Include="System.Data" /^>
echo     ^<Reference Include="System.Deployment" /^>
echo     ^<Reference Include="System.Xml" /^>
echo     ^<Reference Include="gxdotnet8"^>
echo      ^<Private^>False^</Private^>
echo    ^</Reference^>
echo   ^</ItemGroup^>
echo   ^<ItemGroup^>
echo     ^<Import Include="Microsoft.VisualBasic" /^>
echo     ^<Import Include="System" /^>
echo     ^<Import Include="System.Collections" /^>
echo     ^<Import Include="System.Collections.Generic" /^>
echo     ^<Import Include="System.Data" /^>
echo     ^<Import Include="System.Diagnostics" /^>
echo     ^<Import Include="gx" /^>
echo   ^</ItemGroup^>
echo   ^<ItemGroup^>
echo     ^<Compile Include="..\..\%1\%2.vb"^>
echo        ^<Link^>%2.vb^</Link^>
echo     ^</Compile^>
echo     ^<Compile Include="My Project\AssemblyInfo.vb" /^>
echo     ^<Compile Include="My Project\Application.Designer.vb"^>
echo       ^<AutoGen^>True^</AutoGen^>
echo       ^<DependentUpon^>Application.myapp^</DependentUpon^>
echo     ^</Compile^>
echo     ^<Compile Include="My Project\Resources.Designer.vb"^>
echo       ^<AutoGen^>True^</AutoGen^>
echo       ^<DesignTime^>True^</DesignTime^>
echo       ^<DependentUpon^>Resources.resx^</DependentUpon^>
echo     ^</Compile^>
echo     ^<Compile Include="My Project\Settings.Designer.vb"^>
echo       ^<AutoGen^>True^</AutoGen^>
echo       ^<DependentUpon^>Settings.settings^</DependentUpon^>
echo       ^<DesignTimeSharedInput^>True^</DesignTimeSharedInput^>
echo     ^</Compile^>
echo   ^</ItemGroup^>
echo   ^<ItemGroup^>
echo     ^<EmbeddedResource Include="My Project\Resources.resx"^>
echo       ^<Generator^>VbMyResourcesResXFileCodeGenerator^</Generator^>
echo       ^<LastGenOutput^>Resources.Designer.vb^</LastGenOutput^>
echo       ^<CustomToolNamespace^>My.Resources^</CustomToolNamespace^>
echo       ^<SubType^>Designer^</SubType^>
echo     ^</EmbeddedResource^>
echo   ^</ItemGroup^>
echo   ^<ItemGroup^>
echo     ^<None Include="My Project\Application.myapp"^>
echo       ^<Generator^>MyApplicationCodeGenerator^</Generator^>
echo       ^<LastGenOutput^>Application.Designer.vb^</LastGenOutput^>
echo     ^</None^>
echo     ^<None Include="My Project\Settings.settings"^>
echo       ^<Generator^>SettingsSingleFileGenerator^</Generator^>
echo       ^<CustomToolNamespace^>My^</CustomToolNamespace^>
echo       ^<LastGenOutput^>Settings.Designer.vb^</LastGenOutput^>
echo     ^</None^>
echo   ^</ItemGroup^>
echo   ^<Import Project="$(MSBuildBinPath)\Microsoft.VisualBasic.targets" /^>
echo   ^<!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
echo        Other similar extension points exist, see Microsoft.Common.targets.
echo   ^<Target Name="BeforeBuild"^>
echo   ^</Target^>
echo   ^<Target Name="AfterBuild"^>
echo   ^</Target^>
echo   --^>
echo ^</Project^>

goto :EOF

rem --- Workspace --------------------------------------------------------------
:sln

echo ﻿
echo Microsoft Visual Studio Solution File, Format Version 9.00
echo # Visual Studio 2005

for %%i in ( %PROJECTS% ) do (
	echo Project^("{F184B08F-C81C-45F6-A57F-5ABD9991F28F}"^) = "%%i", "vb80\%%i\%%i.vbproj", "{DB24D70A-6E03-431f-AB73-3075CF7A2E3A}"
	echo EndProject
)

echo EndProject
echo Global
echo 	GlobalSection(SolutionConfigurationPlatforms) = preSolution
echo 		Debug^|x64 = Debug^|x64
echo 		Release^|x64 = Release^|x64
echo 	EndGlobalSection
echo 	GlobalSection(ProjectConfigurationPlatforms) = postSolution
for %%i in ( %PROJECTS% ) do (
	echo 		{DB24D70A-6E03-431f-AB73-3075CF7A2E3A}.Debug^|x64.ActiveCfg = Debug^|x64
	echo 		{DB24D70A-6E03-431f-AB73-3075CF7A2E3A}.Debug^|x64.Build.0 = Debug^|x64
	echo 		{DB24D70A-6E03-431f-AB73-3075CF7A2E3A}.Release^|x64.ActiveCfg = Release^|x64
	echo 		{DB24D70A-6E03-431f-AB73-3075CF7A2E3A}.Release^|x64.Build.0 = Release^|x64
)
echo 	EndGlobalSection
echo 	GlobalSection(SolutionProperties) = preSolution
echo 		HideSolutionNode = FALSE
echo 	EndGlobalSection
echo EndGlobal

goto :EOF

rem --- Application.myapp --------------------------------------------------------
:Application_myapp

echo ﻿^<?xml version="1.0" encoding="utf-8"?^>
echo ^<MyApplicationData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"^>
echo   ^<MySubMain^>false^</MySubMain^>
echo   ^<SingleInstance^>false^</SingleInstance^>
echo   ^<ShutdownMode^>0^</ShutdownMode^>
echo   ^<EnableVisualStyles^>true^</EnableVisualStyles^>
echo   ^<AuthenticationMode^>0^</AuthenticationMode^>
echo   ^<ApplicationType^>2^</ApplicationType^>
echo   ^<SaveMySettingsOnExit^>true^</SaveMySettingsOnExit^>
echo ^</MyApplicationData^>

goto :EOF

rem --- Application.Designer.vb --------------------------------------------------------
:Application_Designer_vb

echo ﻿'------------------------------------------------------------------------------
echo ' ^<auto-generated^>
echo '     This code was generated by a tool.
echo '     Runtime Version:2.0.50727.42
echo '
echo '     Changes to this file may cause incorrect behavior and will be lost if
echo '     the code is regenerated.
echo ' ^</auto-generated^>
echo '------------------------------------------------------------------------------

echo Option Strict On
echo Option Explicit On

goto :EOF

rem --- AssemblyInfo.vb --------------------------------------------------------
:AssemblyInfo_vb

echo Imports System
echo Imports System.Reflection
echo Imports System.Runtime.InteropServices

echo ' General Information about an assembly is controlled through the following 
echo ' set of attributes. Change these attribute values to modify the information
echo ' associated with an assembly.

echo ' Review the values of the assembly attributes

echo ^<Assembly: AssemblyTitle("%1")^> 
echo ^<Assembly: AssemblyDescription("")^> 
echo ^<Assembly: AssemblyCompany("ARH Inc.")^> 
echo ^<Assembly: AssemblyProduct("%1")^> 
echo ^<Assembly: AssemblyCopyright("Copyright � ARH Inc. 2011")^> 
echo ^<Assembly: AssemblyTrademark("")^> 
echo ^<Assembly: ComVisible(False)^>

echo 'The following GUID is for the ID of the typelib if this project is exposed to COM
echo ^<Assembly: Guid("0cab14ac-d4b6-4a42-b74e-5c57124c86d9")^> 

echo ' Version information for an assembly consists of the following four values:
echo '
echo '      Major Version
echo '      Minor Version 
echo '      Build Number
echo '      Revision
echo '
echo ' You can specify all the values or you can default the Build and Revision Numbers 
echo ' by using the '*' as shown below:
echo ' ^<Assembly: AssemblyVersion("7.2.*")^> 

echo ^<Assembly: AssemblyVersion("7.2.10.0")^> 
echo ^<Assembly: AssemblyFileVersion("7.2.10.0")^> 

goto :EOF

rem --- Resources.resx --------------------------------------------------------
:Resources_resx

echo ﻿^<?xml version="1.0" encoding="utf-8"?^>
echo ^<root^>
echo   ^<!-- 
echo     Microsoft ResX Schema 

echo     Version 2.0  

echo     The primary goals of this format is to allow a simple XML format 
echo     that is mostly human readable. The generation and parsing of the 
echo     various data types are done through the TypeConverter classes 
echo     associated with the data types.    

echo     Example:    

echo     ... ado.net/XML headers ^& schema ...
echo     ^<resheader name="resmimetype"^>text/microsoft-resx^</resheader^>
echo     ^<resheader name="version"^>2.0^</resheader^>
echo     ^<resheader name="reader"^>System.Resources.ResXResourceReader, System.Windows.Forms, ...^</resheader^>
echo     ^<resheader name="writer"^>System.Resources.ResXResourceWriter, System.Windows.Forms, ...^</resheader^>
echo     ^<data name="Name1"^>^<value^>this is my long string^</value^>^<comment^>this is a comment^</comment^>^</data^>
echo     ^<data name="Color1" type="System.Drawing.Color, System.Drawing"^>Blue^</data^>
echo     ^<data name="Bitmap1" mimetype="application/x-microsoft.net.object.binary.base64"^>
echo         ^<value^>[base64 mime encoded serialized .NET Framework object]^</value^>
echo     ^</data^>
echo     ^<data name="Icon1" type="System.Drawing.Icon, System.Drawing" mimetype="application/x-microsoft.net.object.bytearray.base64"^>
echo         ^<value^>[base64 mime encoded string representing a byte array form of the .NET Framework object]^</value^>
echo         ^<comment^>This is a comment^</comment^>
echo     ^</data^>               

echo     There are any number of "resheader" rows that contain simple 
echo     name/value pairs.    
echo     Each data row contains a name, and value. The row also contains a 
echo     type or mimetype. Type corresponds to a .NET class that support 
echo     text/value conversion through the TypeConverter architecture. 
echo     Classes that don't support this are serialized and stored with the 
echo     mimetype set.    

echo     The mimetype is used for serialized objects, and tells the 
echo     ResXResourceReader how to depersist the object. This is currently not 
echo     extensible. For a given mimetype the value must be set accordingly:    

echo     Note - application/x-microsoft.net.object.binary.base64 is the format 
echo     that the ResXResourceWriter will generate, however the reader can 
echo     read any of the formats listed below.    

echo     mimetype: application/x-microsoft.net.object.binary.base64
echo     value   : The object must be serialized with 
echo             : System.Serialization.Formatters.Binary.BinaryFormatter
echo             : and then encoded with base64 encoding.    
  
echo     mimetype: application/x-microsoft.net.object.soap.base64
echo     value   : The object must be serialized with 
echo             : System.Runtime.Serialization.Formatters.Soap.SoapFormatter
echo             : and then encoded with base64 encoding.
  
echo     mimetype: application/x-microsoft.net.object.bytearray.base64
echo     value   : The object must be serialized into a byte array 
echo             : using a System.ComponentModel.TypeConverter
echo             : and then encoded with base64 encoding.
echo     --^>
echo   ^<xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata"^>
echo     ^<xsd:element name="root" msdata:IsDataSet="true"^>
echo       ^<xsd:complexType^>
echo         ^<xsd:choice maxOccurs="unbounded"^>
echo           ^<xsd:element name="metadata"^>
echo             ^<xsd:complexType^>
echo               ^<xsd:sequence^>
echo                 ^<xsd:element name="value" type="xsd:string" minOccurs="0" /^>
echo               ^</xsd:sequence^>
echo               ^<xsd:attribute name="name" type="xsd:string" /^>
echo               ^<xsd:attribute name="type" type="xsd:string" /^>
echo               ^<xsd:attribute name="mimetype" type="xsd:string" /^>
echo             ^</xsd:complexType^>
echo           ^</xsd:element^>
echo           ^<xsd:element name="assembly"^>
echo             ^<xsd:complexType^>
echo               ^<xsd:attribute name="alias" type="xsd:string" /^>
echo               ^<xsd:attribute name="name" type="xsd:string" /^>
echo             ^</xsd:complexType^>
echo           ^</xsd:element^>
echo           ^<xsd:element name="data"^>
echo             ^<xsd:complexType^>
echo               ^<xsd:sequence^>
echo                 ^<xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" /^>
echo                 ^<xsd:element name="comment" type="xsd:string" minOccurs="0" msdata:Ordinal="2" /^>
echo               ^</xsd:sequence^>
echo               ^<xsd:attribute name="name" type="xsd:string" msdata:Ordinal="1" /^>
echo               ^<xsd:attribute name="type" type="xsd:string" msdata:Ordinal="3" /^>
echo               ^<xsd:attribute name="mimetype" type="xsd:string" msdata:Ordinal="4" /^>
echo             ^</xsd:complexType^>
echo           ^</xsd:element^>
echo           ^<xsd:element name="resheader"^>
echo             ^<xsd:complexType^>
echo               ^<xsd:sequence^>
echo                 ^<xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" /^>
echo               ^</xsd:sequence^>
echo               ^<xsd:attribute name="name" type="xsd:string" use="required" /^>
echo             ^</xsd:complexType^>
echo           ^</xsd:element^>
echo         ^</xsd:choice^>
echo       ^</xsd:complexType^>
echo     ^</xsd:element^>
echo   ^</xsd:schema^>
echo   ^<resheader name="resmimetype"^>
echo     ^<value^>text/microsoft-resx^</value^>
echo   ^</resheader^>
echo   ^<resheader name="version"^>
echo     ^<value^>2.0^</value^>
echo   ^</resheader^>
echo   ^<resheader name="reader"^>
echo     ^<value^>System.Resources.ResXResourceReader, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^</value^>
echo   ^</resheader^>
echo   ^<resheader name="writer"^>
echo     ^<value^>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^</value^>
echo   ^</resheader^>
echo ^</root^>

goto :EOF

rem --- Resources.Designer.vb --------------------------------------------------------
:Resources_Designer_vb

echo ﻿'------------------------------------------------------------------------------
echo ' ^<auto-generated^>
echo '     This code was generated by a tool.
echo '     Runtime Version:2.0.50727.42
echo '
echo '     Changes to this file may cause incorrect behavior and will be lost if
echo '     the code is regenerated.
echo ' ^</auto-generated^>
echo '------------------------------------------------------------------------------
  
echo Option Strict On
echo Option Explicit On
  
  
echo Namespace My.Resources   
  
echo     'This class was auto-generated by the StronglyTypedResourceBuilder
echo     'class via a tool like ResGen or Visual Studio.
echo     'To add or remove a member, edit your .ResX file then rerun ResGen
echo     'with the /str option, or rebuild your VS project.
echo     '^<summary^>
echo     '  A strongly-typed resource class, for looking up localized strings, etc.
echo     '^</summary^>
echo     ^<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"),  _
echo      Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
echo      Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
echo      Global.Microsoft.VisualBasic.HideModuleNameAttribute()^>  _
echo     Friend Module Resources        
  
echo         Private resourceMan As Global.System.Resources.ResourceManager        
  
echo         Private resourceCulture As Global.System.Globalization.CultureInfo
echo         '^<summary^>
echo         '  Returns the cached ResourceManager instance used by this class.
echo         '^</summary^>
echo         ^<Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)^>  _
echo         Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
echo             Get
echo                 If Object.ReferenceEquals(resourceMan, Nothing) Then
echo                     Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("%1.Resources", GetType(Resources).Assembly)
echo                     resourceMan = temp
echo                 End If
echo                 Return resourceMan
echo             End Get
echo         End Property        
  
echo         '^<summary^>
echo         '  Overrides the current thread's CurrentUICulture property for all
echo         '  resource lookups using this strongly typed resource class.
echo         '^</summary^>
echo         ^<Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)^>  _
echo         Friend Property Culture() As Global.System.Globalization.CultureInfo
echo             Get
echo                 Return resourceCulture
echo             End Get
echo             Set(ByVal value As Global.System.Globalization.CultureInfo)
echo                 resourceCulture = value
echo             End Set
echo         End Property
echo     End Module
echo End Namespace

goto :EOF

rem --- Settings.settings --------------------------------------------------------
:Settings_settings

echo ﻿^<?xml version='1.0' encoding='utf-8'?^>
echo ^<SettingsFile xmlns="http://schemas.microsoft.com/VisualStudio/2004/01/settings" CurrentProfile="(Default)" UseMySettingsClassName="true"^>
echo   ^<Profiles^>
echo     ^<Profile Name="(Default)" /^>
echo   ^</Profiles^>
echo   ^<Settings /^>
echo ^</SettingsFile^>

goto :EOF

rem --- Settings.Designer.vb --------------------------------------------------------
:Settings_Designer_vb

echo ﻿'------------------------------------------------------------------------------
echo ' ^<auto-generated^>
echo '     This code was generated by a tool.
echo '     Runtime Version:2.0.50727.42
echo '
echo '     Changes to this file may cause incorrect behavior and will be lost if
echo '     the code is regenerated.
echo ' ^</auto-generated^>
echo '------------------------------------------------------------------------------
  
  
echo Option Strict On
echo Option Explicit On
  
echo Namespace My
  
echo     ^<Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
echo      Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0"),  _
echo      Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)^>  _
echo     Partial Friend NotInheritable Class MySettings
echo         Inherits Global.System.Configuration.ApplicationSettingsBase        
  
echo         Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings), MySettings)        
  
echo #Region "My.Settings Auto-Save Functionality"
echo #If _MyType = "WindowsForms" Then
echo         Private Shared addedHandler As Boolean
  
echo         Private Shared addedHandlerLockObject As New Object
  
echo         ^<Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)^> _
echo         Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
echo             If My.Application.SaveMySettingsOnExit Then
echo                 My.Settings.Save()
echo             End If
echo         End Sub
echo #End If
echo #End Region
  
echo         Public Shared ReadOnly Property [Default]() As MySettings
echo             Get
                 
echo #If _MyType = "WindowsForms" Then
echo                    If Not addedHandler Then
echo                         SyncLock addedHandlerLockObject
echo                             If Not addedHandler Then
echo                                 AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
echo                                 addedHandler = True
echo                             End If
echo                         End SyncLock
echo                     End If
echo #End If
echo                 Return defaultInstance
echo             End Get
echo         End Property
echo     End Class
echo End Namespace
  
echo Namespace My
      
echo     ^<Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
echo      Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
echo      Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()^>  _
echo     Friend Module MySettingsProperty          
echo         ^<Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")^>  _
echo         Friend ReadOnly Property Settings() As Global.%1.My.MySettings
echo             Get
echo                 Return Global.%1.My.MySettings.Default
echo             End Get
echo         End Property
echo     End Module
echo End Namespace

goto :EOF
