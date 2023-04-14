import numpy as np

server_pts = [
    [3.51, 12.01, -23.60],
    [-54.49, 12.01, -23.60],
    [-54.49, -17.99, -23.60],
    [3.51, -17.99, -23.60],
    [3.51, 12.01, 3.20],
    [-54.49, 12.01, 3.20],
    [-54.49, -17.99, 3.20],
    [3.51, -17.99, 3.20],
]

client_pts = [
    [20, 30, -20],
    [-20, 30, -20],
    [-20, 0, -20],
    [20, 0, -20],
    [20, 30, 20],
    [-20, 30, 20],
    [-20, 0, 20],
    [20, 0, 20],
]

src = np.array(server_pts)
tgt = np.array(client_pts)
N = src.shape[0]

src_h = np.hstack((src, np.ones((N, 1))))
A = np.array([np.block([
                [src_h[n], np.zeros(4), np.zeros(4), -tgt[n, 0] * src_h[n]],
                [np.zeros(4), src_h[n], np.zeros(4), -tgt[n, 1] * src_h[n]],
                [np.zeros(4), np.zeros(4), src_h[n], -tgt[n, 2] * src_h[n]]
            ]) for n in range(N)]).reshape(3 * N, 16)
[_, _, V] = np.linalg.svd(A)
H = V.T[:, -1].reshape(4, 4)
H /= H[-1, -1]
print('Server received should multiply:\n', np.linalg.inv(H))
print('Client received should multiply:\n', H)
