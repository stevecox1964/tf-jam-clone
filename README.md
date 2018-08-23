# tf-jam-clone
Learn how to use Tensorflow inside Unity using C# code
This project was modified from the original tf-jam project located at https://github.com/abehaskins/tf-jam
The original tf-jam project used Tensorflow.JS which I did not want to bother with.

In my version we are actually building a Keras model, training the model to fit a simple non-linear dataset using data from
a previsouly run Unity training session file (shots_csv), then converting the model to Tensorflow, then loading the TF model
into Unity using TensorflowSharp C# wrapper.

The TF model will then be queried for "Force" values in real-time which will be used to make basket ball shots.

The project has a jupyter notebook which can be used to load the training and export the model in one pass.

This project uses TensorflowSharp plug-in that you will need to download and import into the Unity project before things
will run properly.

# Getting Project up and running - Download TFSharp and Install
Goto here: https://github.com/llSourcell/Unity_ML_Agents/blob/master/docs/Using-TensorFlow-Sharp-in-Unity-(Experimental).md
to get the TensorflowSharp Unity package, download the package and copy into your tf-jam-clone main directory.
Once you have Unity up and running and have loaded the tf-jam-clone project, Select main menu, "Assests/Import Package/Custom Package
then select the "TFSharpPackage.unitypackage" file you previsouly downloaded.
You will eventually be prompted to select all items, or Android/Computer/iOS, I just un-select Android/iOS which are not needed at this time.

# Running for first time
If all goes well, Unity should show no signs of compiler errors, so you should be able to just run the project and
start seeing a Basket Ball Hoop and a cube that is taking shots. You can move the arrow keys around to see the cube
try and make shots from different positions. This is using a model I trained. I will be posting videos on you tube
on how to do a better job of training the model. But you will need to install Python/Tensorflow/Keras before all that.


# Getting the model building pipline working - Install Python 3.6/Tensorflow 1.9 and Keras 2.1.6
In order to "build and tune your shot model, you will need to install a bunch of Tensorflow/Keras/etc dependancies.
I am going to try and fill this section out to the best of my abilites so people can start building their own models.

pip install jupyter
pip install numpy
pip install pandas
pip install matplotlib
pip install tensorflow==1.9
pip install -I keras==2.1.6

This should at least get you going to get the jupyter note book up and running and at least "see" the Python code
to get the model trained from newly created "shots" data


More to come





