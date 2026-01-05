# HSLU SD — Experience 2 (2026)

- **Open the project:** Install Unity Editor 6.3 LTS (or a compatible 6.x). In Unity Hub, click **Add**, select this repository folder, then open it. Allow Unity to import assets and packages.

- **Switch platform to Android:** In Unity Editor go to File → Build Settings. Select **Android** and click **Switch Platform**. If Android Build Support (SDK/NDK/OpenJDK) is not available, install it via Unity Hub before switching.

- **Test in the Unity Editor: Connect Quest in Link mode:**
	1. Use a USB-C cable (or Air Link) to connect the Quest to your PC.
	2. Put on the headset and accept the data access prompt if shown.
	3. In the headset, choose **Enable Oculus Link**. In Unity, set Play mode to use the Link device; pressing Play runs the Editor content streamed to the headset.

- **Run on Headset: Build & install to Quest**
    1. Create a Meta developer account: https://developers.meta.com/horizon/documentation/native/android/mobile-device-setup/
	2. Enable Developer Mode on your headset: open the Meta/Oculus mobile app → select your headset → Settings → More Settings → Developer Mode → toggle on.
	3. Connect the Quest via USB to the PC and accept any USB debugging/allow prompts in the headset.
	4. In Unity: File → Build Settings → select your Android device (it should appear in the Run Device list). Click **Build And Run** to install the APK to the headset.

Troubleshooting

- If the device does not appear when building, verify Developer Mode is enabled, the USB cable supports data, and that you accepted the headset's USB debugging prompt. Restart the computer and/or headset if nothing else works.
- If XR content does not stream in Link mode, check the Oculus PC app is running and Link/Air Link is enabled and paired.
- Ensure that that 'Oculus' is enabled in the Unity Editor under Edit → Project settings → XR Plugin-Management for the platforms PC and Android
