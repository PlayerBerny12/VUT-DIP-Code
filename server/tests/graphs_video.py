"""
DFDF (DeepfakeDetectionFramework)
Author: Jan Bernard (xberna18@stud.fit.vutbr.cz)
"""

import sys
import pandas as pd
import matplotlib.pyplot as plt


from graphs_core import plot, calcualte_roc_auc

# Read csv data file
df = pd.read_csv(sys.argv[1], delimiter=";", header=None)

print(f"Size avg: {df[3].mean()}")
print(f"Size median: {df[3].median()}")
print(f"Size min: {df[3].min()}")
print(f"Size max: {df[3].max()}")
print(f"Time avg: {df[4].mean()}")
print(f"Time median: {df[4].median()}")
print(f"Time min: {df[4].min()}")
print(f"Time max: {df[4].max()}")
print()

# Plot ROC + AUC for all detection methods
plt.rcParams.update({'font.size': 12})
plt.figure(figsize=(7,7))

# ShallowCNN_lfcc_I
calcualte_roc_auc(df[0], df[1], "fakeVideoForensics")

# Show plot
plot()