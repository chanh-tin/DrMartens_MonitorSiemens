:: Chạy file này để tự động tạo thư mục và cấu trúc file
:: Kịch bản như sau: 
::   Tạo các thư mục sau nếu chưa tồn tại : 
::     artifacts, builddocs, lib, packages, samples, src, tests, orthers
::   Tạo các file sau nếu chưa tồn tại
::     .editorconfig, .gitignore, .gitattributes, build.cmd, build.sh, LICENSE, NuGet.Config, README.md


:: Main scripts
:: Tạo các thư mục sau nếu chưa tồn tại

::set folders[0]=(artifacts builddocs lib packages samples src tests orthers)
@echo off
setlocal enabledelayedexpansion

cls

echo IMAG .NET structure v0.0.0 (30/12/22)
echo -------------------------------------

echo:
echo 1 - Create Folders (artifacts, builddocs, lib, packages, samples, src, tests, orthers)
set folders[0]=artifacts
set folders[1]=build
set folders[2]=docs
set folders[3]=libs
set folders[4]=packages
set folders[5]=samples
set folders[6]=src
set folders[7]=tests
set folders[8]=orthers

:: FOR /L %%variable IN (lowerlimit,Increment,Upperlimit) DO do_something

for /l %%n in (0,1,8) do ( 
   IF not exist !folders[%%n]! (
   		echo Create new  [!folders[%%n]!] folder
   		mkdir !folders[%%n]!
   		if "!errorlevel!" EQU "0" (
    		echo --- Folder created successfully
  		) else (
    		echo --- Error while creating folder
  		)
   ) ELSE (
   		echo Folder [!folders[%%n]!] already exists
   )
)

:: tạo các file cần thiết
:: ( .editorconfig, .gitignore, .gitattributes, build.cmd, build.sh, LICENSE, NuGet.Config, README.md )
echo:
echo 1 - Create Files ( .editorconfig, .gitignore, .gitattributes, build.cmd, build.sh, LICENSE, NuGet.Config, README.md )

set files[0]=.editorconfig
set files[1]=.gitignore
set files[2]=.gitattributes
set files[3]=build.cmd
set files[4]=build.sh
set files[5]=LICENSE
set files[6]=NuGet.Config
set files[7]=README.md

for /l %%n in (0,1,7) do ( 
   IF not exist !files[%%n]! (
   		echo Create new [!files[%%n]!] file
   		echo '' >> !files[%%n]!
   		if "!errorlevel!" EQU "0" (
    		echo --- file created successfully
  		) else (
    		echo --- Error while creating file
  		)
   ) ELSE (
   		echo file [!files[%%n]!] already exists
   )
)

echo DONE
:: thêm nội dung cho file từng loại file
:: NuGet.Config
echo '<configuration>' >> NuGet.Config
echo '    <packageRestore>' >> NuGet.Config
echo '        <add key="enabled" value="True" />' >> NuGet.Config
echo '    <packageRestore>' >> NuGet.Config
echo '<configuration>' >> NuGet.Config

:: đang cập nhật
pause
exit