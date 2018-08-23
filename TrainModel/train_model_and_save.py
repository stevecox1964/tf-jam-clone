

import pandas as pd

import numpy as np
import keras

from matplotlib import pyplot as plt

from keras.layers import Input, Dense
from keras.models import Model

from sklearn.preprocessing import StandardScaler, MinMaxScaler, Normalizer

shots = pd.read_csv("successful_shots.csv")

print(shots.columns,shots.dtypes)

shots.head()


x = shots["dist"].values.reshape(-1,1).astype(np.float64)
y = shots["force"].values.reshape(-1,1).astype(np.float64)

x.shape,y.shape


sc = StandardScaler()
x_ = sc.fit_transform(x)
y_ = sc.fit_transform(y)

inputs = Input(shape=(1,))
preds = Dense(1,activation='linear')(inputs)

model = Model(inputs=inputs,outputs=preds)
sgd=keras.optimizers.SGD()
model.compile(optimizer=sgd ,loss='mse')
model.fit(x_,y_, batch_size=1, verbose=1, epochs=10, shuffle=False)

model.save('shotsmodel.h5')

