import matplotlib.pyplot as plt
from sklearn import metrics

def calcualte_roc_auc(df_true, df_predict, name):
    fpr, tpr, _ = metrics.roc_curve(df_true, df_predict)
    auc = metrics.roc_auc_score(df_true, df_predict)
    plt.plot(fpr, tpr, label=name)
    print(f"{name} AUC = {auc}")

def plot():
    plt.ylabel("True Positive Rate")
    plt.xlabel("False Positive Rate")
    plt.legend(bbox_to_anchor=(0.5, -0.11), loc='upper center', ncol=3)
    plt.tight_layout()
    plt.show()

def calculate_global_score1(row):
    if row["below"] == row["above"]:
        val = row["avg"]
    elif row["below"] > row["above"]:
        val = (row["sum"] + (row["min"] * (row["count"] - 1))) / ((row["count"] * 2) - 1)
    else:
        val = (row["sum"] + (row["max"] * (row["count"] - 1))) / ((row["count"] * 2) - 1)

    return val

def calculate_global_score2(row):
    val = row["avg"]
    
    return val

def calculate_global_score3(row):
    val = (row["sum"] + (row["min"] * (row["count"] - 1))) / ((row["count"] * 2) - 1)

    return val

def calculate_global_score4(row):
    val = (row["sum"] + (row["max"] * (row["count"] - 1))) / ((row["count"] * 2) - 1)

    return val