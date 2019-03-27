# 5236AndroidVoiceControlGame
<br> **Embed Unity in Android**
<br> export the project as Gradle https://medium.com/@davidbeloosesky/embedded-unity-within-android-app-7061f4f473a
<br> do not change anything after export
<br> modify the menifest.xml to include other activities
<br> resovele dependency by checking the dependency tree
<br> copy other activity.java to main, and add layout, strings ,color and style in res
<br> add google-service at the root of the project folder
<br> fix build.gradle of Firebase

<br> **add firebase SDK**
<br> Project-level build.gradle (<project>/build.gradle):

<br>buildscript {
 <br> dependencies {
 <br>   // Add this line
    <br> classpath 'com.google.gms:google-services:4.0.1'
 <br>  }
 <br>}
 <br>App-level build.gradle (<project>/<app-module>/build.gradle):

 <br>dependencies {
  <br> // Add this line
  <br> implementation 'com.google.firebase:firebase-core:16.0.1'
 <br>}

 <br>// Add to the bottom of the file
 <br>apply plugin: 'com.google.gms.google-services'
 <br>Includes Analytics by default 
 <br>Finally, press "Sync now" in the bar that appears in the IDE:

<br> **References**
<br> [1] Android Open Source Project, Android, http://developers.android.com
<br> [2] Firebase, https://firebase.google.com/
<br> [3] Pusheen the Cat, http://pusheen.com/page/2
