set adb="c:\Program Files (x86)\Android\android-sdk\platform-tools\adb.exe'
adb -s emulator-5554 forward tcp:9867 tcp:9867
pushd FabulousDemo
fabulous --watch --send
popd

