set adb = "C:\Program Files (x86)\Android\android-sdk\platform-tools\adb.exe"
adb -e forward tcp:9867 tcp:9867

pushd FabulousDemo
fabulous --watch --send
popd