# Create React App Template Utility - Visual Studio

This little utility allows you to create templates for your react app.  It can be thought of as the "File->New" of any new project.  

The utility:

* Uses the official create-react-app to generate a react app
* It allows you to pass any flags through you want (free text)
* Sets up any extra npm packages that you routinely install
* Sets up your project directory structure
* Copies over any boiler plate code you want for this project type
* Generates a visual solution & project file
* In Visual Studio, it sets up the "build/debug" command to run "npm start"
* In Visual Studio, it sets up the "build/release" command to "npm run build"
* The tool allows you to save configs and re-load them again at a later date. This means you could have different conifgs for different app types.
* The app template files are simple .json files

# Screenshot

![alt tag](https://raw.githubusercontent.com/OceanAirdrop/CreateReactAppVisualStudio/master/Screenshots/CreateReactApp.gif)

# Project Origins

The origins of this side-project stem from a stack overflow question I asked about how to extend the build process in Visual Studio.

Once I found out how to do this and because I wanted to generate a visual studio solution for every new React project I create, I knocked up this quick tool which wraps up everything I wanted all together.

https://stackoverflow.com/questions/58733134/how-to-debug-a-react-app-in-visual-studio-2019-using-the-blank-node-js-templat
