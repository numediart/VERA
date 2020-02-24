
Requirements:

 * Minimum Unity version 2018.2.5f1

To get started developing, visit  https://vr.tobii.com/sdk

Quick Start:

See sample scene Example_GettingStarted

Changes for v1.6.0

* Tobii Settings menu replaced with a PropertyDrawer for TobiiXR_Settings

* Added a TobiiXR Initializer prefab that holds a TobiiXR_Settings property and uses it to initialize TobiiXR

Changes for v1.5.3

* Fixed avatar facial animations in newer versions of Unity

Changes for v1.5.2

* Updated G2OM to version 6.2.0

Changes for v1.5.1

* Updated HTC Provider to use SR Anipal SDK 1.1.0.1

Changes for v1.5.0

* Updated Stream Engine to version 4.0.0

* Updated G2OM to version 6.1.0

* Added better local to world transform for gaze when using OpenVR

* Gaze Modifier was changed from being a provider to being a filter that gets applied to the TobiiXR facade

* Improved handling of multiple cameras and disabled cameras


Changes for v1.4.1

* Fixed native library includes for macOS


Changes for v1.4.0

* Updated Stream Engine to version 3.3.0

* Replaced eye openness with blink

* Removed left and right gaze vectors

* Added convergence distance for avatar animations

* Fixed timestamps for Tobii provider and HTC provider


Changes for v1.3.0:

* Added HTC provider to support HTC Vive Pro Eye

* Added Tobii HTC provider that chooses Tobii or HTC provider at runtime depending on connected HMD

* Compensates for predicted head movements when transforming gaze data to world space

* Fixed leaking handles when multiple eye trackers are connected to the machine


Changes for v1.2.7:

* Fixed collider size issue with ui sliders


Changes for v1.2.6:

 * Added popup for license


Changes for v1.2.4:

 * Removed dependencies on Examples

 * Removed dependecies on DevTools

 * Exposed StreamEngineContext for calibration

 * Added Assemblydefinitions


Changes for v1.2.3:

 * Bugfix DevTools

 * Rename DevKit -> DevTools

 * Rename Gaze Emulator -> Gaze Provider

Changes for v1.1.0:

 * Added Field of use

Changes for v1.0.0:

 * Removed OpenVRProvider

 * Removed SVR Provider

 * Added Mouse Provider
 
 * Added Dev Tools GUI
 
 * Added Dev Tool: GazeModifier 
 
 * Added Dev Tool: GazeVisualizer

 * Added Examples

 * Updated G2OM
 

Changes for v0.7.0:

 * Changed default provider from NoseProvider to OpenVRProvider 
 
 * TobiiXR_Settings fix saved when altered
 
 * Bugfix: Changing location of TobiiXR-folder now works

Changes for v0.6.0:

 * Updated G2OM
 
 * Updated streamengine to v. 3.0.4

 * Added readme (this).

Changes for v0.5.2:

 * Fixed issues for automatic builds.

Changes for v0.5.1:

 * Fixed issue with layermask for G2OM.

Changes for v0.5.0:

 * Custom window for Tobii settings.

 * Platform specific gaze Providers.  

Changes for v 0.4.1

 * Added Android defines for SVRProvider.

 * Fixed issue with faulty transform of gaze data.

Changes for vb 0.4.0

 * Added setting file to change provider and/or G2OM settings. It can be viewed under the Tobii menu item.

 * Added provider for SVR.

Changes for v0.3.0

 * G2OM examples now use Tobii XR.

 * Made it possible to set instance of G2OM.

 * Added debug visualization component for G2OM.

 * Added editor utility script to generate collider for UI elements.

Changes for v0.2.0

 * Initial release.
