import sys
import pandas as pd
import matplotlib.pyplot as plt

from graphs_core import plot, calcualte_roc_auc, calculate_global_score1, calculate_global_score2, \
    calculate_global_score3, calculate_global_score4

# Read csv data file
df = pd.read_csv(sys.argv[1], delimiter=";", header=None)

# Calculate additional values
df["below"] = (df.loc[:, 2:7] < 0.5).sum(axis=1)
df["above"] = (df.loc[:, 2:7] > 0.5).sum(axis=1)
df["min"] = df.loc[:, 2:7].min(axis=1)
df["max"] = df.loc[:, 2:7].max(axis=1)
df["avg"] = df.loc[:, 2:7].mean(axis=1)
df["sum"] = df.loc[:, 2:7].sum(axis=1)
df["count"] = 6
df["score1"] = df.apply(calculate_global_score1, axis=1)
df["score2"] = df.apply(calculate_global_score2, axis=1)
df["score3"] = df.apply(calculate_global_score3, axis=1)
df["score4"] = df.apply(calculate_global_score4, axis=1)

# Plot ROC + AUC for all detection methods
plt.figure(figsize=(8,5))

# ShallowCNN_lfcc_I
calcualte_roc_auc(df[0], df[2], "ShallowCNN_lfcc_I")

# ShallowCNN_lfcc_O
calcualte_roc_auc(df[0], df[3], "ShallowCNN_lfcc_O")

# ShallowCNN_mfcc_I
calcualte_roc_auc(df[0], df[4], "ShallowCNN_mfcc_I")

# ShallowCNN_mfcc_O
calcualte_roc_auc(df[0], df[5], "ShallowCNN_mfcc_O")

# TSSD_Wave_I
calcualte_roc_auc(df[0], df[6], "TSSD_Wave_I")

# TSSD_Wave_O
calcualte_roc_auc(df[0], df[7], "TSSD_Wave_O")

# Show plot
plot()

# Clear and plot overall score
plt.close()
plt.figure(figsize=(8,5))

# Overall score1
calcualte_roc_auc(df[0], df["score1"], "Overall score1")

# Overall score1
calcualte_roc_auc(df[0], df["score2"], "Overall score2")

# Overall score1
calcualte_roc_auc(df[0], df["score3"], "Overall score3")

# Overall score1
calcualte_roc_auc(df[0], df["score4"], "Overall score4")

# Show plot
plot()