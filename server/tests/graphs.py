import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
from sklearn import metrics
import matplotlib.pyplot as plt

"""
    int responsesCountBelow = responses.Count(x => x.Value < 0.5);
    int responsesCountAbove = responses.Count(x => x.Value > 0.5);
    double? responsesMinValue = responses.Min(x => x.Value);
    double? responsesMaxValue = responses.Max(x => x.Value);
    double? responsesAvgValue = responses.Average(x => x.Value);
    double? responsesSumValue = responses.Sum(x => x.Value);
    double? responsesGlobalValue = null;

    if (responsesCountBelow == responsesCountAbove)
    {
        responsesGlobalValue = responsesAvgValue;
    }
    else
    {
        if (responsesCountBelow > responsesCountAbove)
        {
            responsesGlobalValue = (responsesSumValue + (responsesMinValue * (responses.Count - 1))) / ((responses.Count * 2) - 1);
        }
        else
        {
            responsesGlobalValue = (responsesSumValue + (responsesMaxValue * (responses.Count - 1))) / ((responses.Count * 2) - 1);
        }
    }
"""

def calculate_global_score(row):
    if row["below"] == row["above"]:
        val = row["avg"]
    elif row["below"] > row["above"]:
        val = (row["sum"] + (row["min"] * (row["count"] - 1))) / ((row["count"] * 2) - 1)
    else:
        val = (row["sum"] + (row["max"] * (row["count"] - 1))) / ((row["count"] * 2) - 1)

    return val

# df = pd.read_csv("output_lj_wf.csv", delimiter=";", header=None)
# df = pd.read_csv("output_asv.csv", delimiter=";", header=None)
df = pd.read_csv("output_cdf_clear.csv", delimiter=";", header=None)
# df = pd.read_csv("output_ff.csv", delimiter=";", header=None)

# df = df.loc[:, [0,1,3,5,7]]
# df["below"] = (df.loc[:, 1:3] < 0.5).sum(axis=1)
# df["above"] = (df.loc[:, 1:3] > 0.5).sum(axis=1)
# df["min"] = df.loc[:, 1:3].min(axis=1)
# df["max"] = df.loc[:, 1:3].max(axis=1)
# df["avg"] = df.loc[:, 1:3].mean(axis=1)
# df["sum"] = df.loc[:, 1:3].sum(axis=1)
# df["count"] = 3
# df["global"] = df.apply(calculate_global_score, axis=1)

fpr, tpr, _ = metrics.roc_curve(df[0], df[1])
auc = metrics.roc_auc_score(df[0], df[1])
# fpr, tpr, _ = metrics.roc_curve(df[0], df[7])
# auc = metrics.roc_auc_score(df[0], df[7])

plt.plot(fpr, tpr, label="AUC="+str(auc))
plt.ylabel('True Positive Rate')
plt.xlabel('False Positive Rate')
plt.legend(loc=4)
plt.show()