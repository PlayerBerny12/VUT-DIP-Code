import sys
import pandas as pd
import matplotlib.pyplot as plt


from graphs_core import plot, calcualte_roc_auc

# Read csv data file
df = pd.read_csv(sys.argv[1], delimiter=";", header=None)

# Plot ROC + AUC for all detection methods
plt.figure(figsize=(8,5))

# ShallowCNN_lfcc_I
calcualte_roc_auc(df[0], df[1], "fakeVideoForensics")

# Show plot
plot()