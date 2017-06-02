# -*- coding: utf-8 -*-
import random
import numpy as np

from collections import deque
from keras.models import Sequential
from keras.layers import Dense
from keras.optimizers import Adam

from simpleOSC import initOSCClient, initOSCServer, setOSCHandler, sendOSCMsg, closeOSC, \
     createOSCBundle, sendOSCBundle, startOSCServer

EPISODES = 20


class DQNAgent:
    def __init__(self, state_size, action_size):
        self.state_size = state_size
        self.action_size = action_size
        self.memory = deque(maxlen=2000)
        self.gamma = 0.95    # discount rate
        self.epsilon = 1.0  # exploration rate
        self.epsilon_min = 0.01
        self.epsilon_decay = 0.995
        self.learning_rate = 0.001
        self.model = self._build_model()

    def _build_model(self):
        # Neural Net for Deep-Q learning Model
        model = Sequential()
        model.add(Dense(24, input_dim=self.state_size, activation='relu'))
        model.add(Dense(24, activation='relu'))
        model.add(Dense(self.action_size, activation='linear'))
        model.compile(loss='mse',
                      optimizer=Adam(lr=self.learning_rate))
        return model

    def remember(self, state, action, reward, next_state, done):
        self.memory.append((state, action, reward, next_state, done))

    def act(self, state):
        if np.random.rand() <= self.epsilon:
            return random.randrange(self.action_size)
        act_values = self.model.predict(state)
        return np.argmax(act_values[0])  # returns action

    def replay(self, batch_size):
        minibatch = random.sample(self.memory, batch_size)
        for state, action, reward, next_state, done in minibatch:
            target = reward
            if not done:
              target = reward + self.gamma * \
                       np.amax(self.model.predict(next_state)[0])
            target_f = self.model.predict(state)
            target_f[0][action] = target
            self.model.fit(state, target_f, epochs=1, verbose=0)
        if self.epsilon > self.epsilon_min:
            self.epsilon *= self.epsilon_decay

    def load(self, name):
        self.model.load_weights(name)

    def save(self, name):
        self.model.save_weights(name)

def inputHandler(addr, tags, data, source):
    # get input from Unity via OSC
    # print "received new osc msg from %s"  
    print "addr : %s" % addr
    #print "typetags :%s" % tags
    print "data: %s" % data


if __name__ == "__main__":  # main function
    # setup Learning Model
    state_size = 14
    num_objects = 30
    num_angle_step = 6
    num_scale_step = 4
    num_dist_step = 4
    num_rotation_bool = 2
    action_size = num_objects * num_angle_step * num_scale_step * num_dist_step * num_rotation_bool

    agent = DQNAgent(state_size, action_size)
    # agent.load("./save/cartpole-master.h5")
    done = False
    batch_size = 32

    # setup OSC parts
    initOSCClient() # takes args : ip, port
    print 'client'
    initOSCServer() # takes args : ip, port, mode --> 0 for basic server, 1 for threading server, 2 for forking server
    print 'server'

    # bind addresses to functions
    setOSCHandler('/inputs', inputHandler)
    print 'check'

    startOSCServer() # and now set it into action

    print 'ready to receive and send osc messages ...'


def outputHandler(data):
    sendOSCMsg("/outputs", data) # !! it sends by default to localhost ip "127.0.0.1" and port 9000


def runModel():
    #for e in range(EPISODES):
    # get input from Unity via OSC
    # print "received new osc msg from %s"
    print "addr : %s" % addr
    #print "typetags :%s" % tags
    print "data: %s" % data

    #state = np.random.normal(size=state_size) # <- Must receive message via OSC. debug only
    state = np.reshape(state, [1, state_size])
    print 'current state (supposedly angles + object id): ', state

    #for time in range(500):
    action = agent.act(state)
    print 'action: ', action
    act_rotate = True if action % num_rotation_bool == 0 else False
    action /= num_rotation_bool
    act_scale = action % num_scale_step
    action /= num_scale_step
    act_dist = action % num_dist_step
    action /= num_dist_step
    act_angle = action % num_angle_step
    action /= num_angle_step
    act_object = action
    print 'object: ', act_object
    print 'angle: ', act_angle
    print 'dist: ', act_dist
    print 'scale: ', act_scale
    print 'rotate: ', act_rotate
    # Must send this action back to Unity via OSC.

    #next_state, reward, done, _ = env.step(action)
    #raw_input('this is where osc sends a message to python ML algorithm. Just type anything here: ')

    # Must update next_state, reward, done via OSC.

    next_state = np.random.normal(size=state_size)

    done = True if next_state[1] < 0 and next_state[2] < 0 and next_state[3] < 0 else False
    reward = 1 if not done else -10

    next_state = np.reshape(next_state, [1, state_size])

    agent.remember(state, action, reward, next_state, done)
    state = next_state

    if done:
        print("episode: {}/{}, score: {}, e: {:.2}"
            .format(e, EPISODES, time, agent.epsilon))
        #if len(agent.memory) > batch_size:
        #    agent.replay(batch_size)
        # if e % 10 == 0:
        #     agent.save("./save/cartpole.h5")
