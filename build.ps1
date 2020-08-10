
$MAPBOX_DOWNLOADS_TOKEN = ""
$MAPBOX_IOS_VERSIOn = "6.0.0"
$MAPBOX_ANDROID_VERSION = "9.3.0"

$mapboxiOSSdkUrl = "https://api.mapbox.com/downloads/v2/mobile-maps/releases/ios/$MAPBOX_IOS_VERSION/mapbox-ios-sdk-dynamic-with-events.zip?access_token=$MAPBOX_DOWNLOADS_TOKEN"
$mapboxAndroidSdkUrl = "https://api.mapbox.com/downloads/v2/mobile-maps/releases/android/$MAPBOX_ANDROID_VERSION/mapbox-android-sdk-all.zip?access_token=$MAPBOX_DOWNLOADS_TOKEN"

New-Item -ItemType Directory -Force -Path "externals"

if (!(Test-Path -Path "externals/mapbox-ios.zip")) {
    Invoke-WebRequest -Uri $mapboxiOSSdkUrl -OutFile "externals/mapbox-ios.zip"
    Expand-Archive -Path "externals/mapbox-ios.zip" -DestinationPath "externals/mapbox-ios/" -Force
}

if (!(Test-Path -Path "externals/mapbox-android.zip")) {
    Invoke-WebRequest -Uri $mapboxAndroidSdkUrl -OutFile "externals/mapbox-android.zip"
    Expand-Archive -Path "externals/mapbox-android.zip" -DestinationPath "externals/mapbox-android/" -Force
}

if ($IsWindows) {
    Start-Process "./MapboxInterop.Native.Android/gradlew.bat" -ArgumentList "build"
} else {
    Start-Process "./MapboxInterop.Native.Android/gradlew" -ArgumentList "build"
}

if ($IsMacOS) {
    Start-Process "xcodebuild" -ArgumentList "-sdk iphoneos -project ./MapboxInterop.Native.iOS/MapboxInterop.xcodeproj"
    Start-Process "xcodebuild" -ArgumentList "-sdk iphonesimulator -project ./MapboxInterop.Native.iOS/MapboxInterop.xcodeproj"
    Start-Process "lipo" -ArgumentList "-create -o ./MapboxInterop.Native.iOS/build/Release-iphoneos/MapboxInterop.framework/MapboxInterop ./MapboxInterop.Native.iOS/build/Release-iphoneos/MapboxInterop.framework/MapboxInterop ./MapboxInterop.Native.iOS/build/Release-iphonesimulator/MapboxInterop.framework/MapboxInterop"
}


