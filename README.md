# 5236AndroidVoiceControlGame
<br> **Embed Unity in Android**
<br> please follow the instruction in this article
<br> https://medium.com/@davidbeloosesky/embedded-unity-within-android-app-7061f4f473a
<br> also after you export the gradle prject of the unity project
<br> comment out the bundle block in the build.gradle file

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


