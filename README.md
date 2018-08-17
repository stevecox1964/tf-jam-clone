# tf-jam-clone
Learn how to use Tensorflow inside Unity use C# code
This project is a modified version of the original tf-jam project located at
https://github.com/abehaskins/tf-jam

I was going to do a fork of the original but I made to many changes and wanted to upload my version.
In my version we are not using Tensorflow.JS but an actual Keras model with TF backend
The Original project has a great Jupyter Notebook showing what the model would do after training. 

I modified the note book to save the Keras model after training. I also added batch scripts to train and
re-deploy the model in order to better integrate the training, testing pipeline.

This project uses a Tensorflow C# plug-in you need to download and import into the Unity project before things
will run properly.


