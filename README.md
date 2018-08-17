# tf-jam-clone
Learn how to use Tensorflow inside Unity using C# code
This project was modified from the original tf-jam project located at
https://github.com/abehaskins/tf-jam

The original tf-jam project used Tensorflow.JS which I did not want to bother with.

In my version we are actualy building Keras model, training it to fit a simple linear model using data from
a previsouly run Unity training session, then converting the model to a Tensorflow model type, and loading into
a C# with the help of TensorflowSharp.

The Original project had a great Jupyter Notebook showing the model fiting the training data. I modified the note book to use Keras.

I also added batch scripts to build/train/convert the Keras into Tensorflow format for better round trip integration with Unity.

This project uses TensorflowSharp plug-in that you will need to download and import into the Unity project before things
will run properly.

Goto here: 
https://github.com/llSourcell/Unity_ML_Agents/blob/master/docs/Using-TensorFlow-Sharp-in-Unity-(Experimental).md
to get the Unity package, download the package and import into this project.
Goto here:
https://github.com/migueldeicaza/TensorFlowSharp 
to check out the code




