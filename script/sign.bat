@ echo off
set file=%1
set times=0

set sn_exe=C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools\x64\sn.exe
set ildasm_exe=C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools\x64\ildasm.exe
set ilasm_exe=C:\Windows\Microsoft.NET\Framework\v2.0.50727\ilasm.exe
set ilasm_exe_v4=C:\Windows\Microsoft.NET\Framework\v4.0.30319\ilasm.exe
set ilasm_exe_x64=C:\Windows\Microsoft.NET\Framework64\v2.0.50727\ilasm.exe
set ilasm_exe_x84_v4=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ilasm.exe
set snk=core.snk

if "%file%"=="" (goto END)
	else
		echo goto check
		goto CHECK

:CHECK
call "%sn_exe%" -q -vf %1 > NUL 
if ERRORLEVEL 1 (goto NOT_STRONG)
	else
		echo goto strong
		goto STRONG

:SIGN
if %times% LSS 2 (
	call "%ildasm_exe%" /out:%file:.dll=.il% %file%
	call "%ilasm_exe_v4%" /dll %file:.dll=.il% /key=%snk%
	set times=times+1
	goto CHECK)
	else
		echo sign failed.
		goto END

:NOT_STRONG
echo Not strong named: %1
echo next step sign this:
goto SIGN
goto CHECK

:STRONG
echo Has strong name: %1
goto END

:END
popd