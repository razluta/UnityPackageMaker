# Unity Package Maker [![License](https://img.shields.io/badge/License-MIT-lightgrey.svg?style=flat)](http://mit-license.org)
![](/Screenshots/PackMak_Header.png)

This repository is a Unity Editor tool for creating new Unity packages (or updating existing ones).

The tool has been verified on the following versions of Unity:
- 2019.4
- 2019.3

*  *  *  *  *

## Setup
##### Option A) Clone or download the repository and drop it in your Unity project.
##### Option B) Add the repository to the package manifest (go in YourProject/Packages/ and open the "manifest.json" file and add "com..." line in the depenencies section). If you don't have Git installed, Unity will require you to install it.
```
{
  "dependencies": {
      ...
      "com.razluta.unitypackagemaker": "https://github.com/razluta/UnityPackageMaker.git"
      ...
  }
}
```
##### Option C) Add the repository to the Unity Package Manager using the Package Manager dropdown.

*  *  *  *  *

## Using the tool
### Option A) Creating a new package
#### Step 01 of 05
Launch the tool from the Unity menus by navigating to _Raz's Tools_ > _Package Maker_.
#### Step 02 of 05
Since you are creating a new package, you can ignore the _Load Existing Unity Package_ button. Start populating the fields. All properties that are enabled by default and do not have the ability to be disabled are mandatory.
When in doubt about the data, review Unity's documentation: [Package Layout Manual](https://docs.unity3d.com/Manual/cus-layout.html) and [Package Manifest Manual](https://docs.unity3d.com/Manual/upm-manifestPkg.html).
#### Step 03 of 05
Under the _Included Package Contents section_, enable the toggles for all additional documentation and folders you would like to include. If you enable any of the folder creation (which is highly recommend to follow Unity's naming standards and keep consistency between projects), keep in mind the tool will also create a .gitnull file to force git to acknowledge the folder while Unity will not see the file and will only see the folder. If you have enabled any additional documents such as the README.md, their text fields on the right side of the tools will be enabled and you can add your text there.
#### Step 04 of 05
You can at any time _Clear All_ fields, which will clear all the data in the tool.
#### Step 05 of 05
Once you are done putting in your data, press _Create New Unity Package_. If all the data is valid, the tool will ask you for the parent folder where you would like the tool to create the root tool folder and all the contents.

### Option B) Updating an existing package
#### Step 01 of 05
Launch the tool from the Unity menus by navigating to _Raz's Tools_ > _Package Maker_.
#### Step 02 of 05
Press the _Load Existing Unity Package_ button and navigate to the root folder of the package from which you would like to extract the data. If Unity can find a valid manifest (package.json), it will auto-populate all the tool fields.
#### Step 03 of 05
Made any edits necessary to the fields, add/remove/modify content.
#### Step 04 of 05
You can at any time _Clear All_ fields, which will clear all the data in the tool.
#### Step 05 of 05
Once you are done with the modifications, press _Update Existing Unity Package_. If all the data is valid, the tool will override the data of the Unity Package you loaded using the _Load Existing Unity Package_.

### Option C) Creating a new package from an existing package 
#### Step 01, 02, 03 and 04 of 05
Same as Option B) Updating an existing package
#### Step 05 of 05
Once you are done with the modifications, press _Create New Unity Package_. If all the data is valid, the tool will ask you for the parent folder where you would like the tool to create the root tool folder and all the contents.

![](/Screenshots/PackMak_Screenshot001.PNG)
