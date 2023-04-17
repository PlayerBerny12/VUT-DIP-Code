from fastapi import FastAPI

app = FastAPI()

def data_preparation():
    """
    Prepare data from request
    
    E.g.: convert to specific format (wav, mp3, ...) 
    or split data to specific frames
    """
    ...

def detection_method():
    """
    Run detection method on prepared data    
    """
    ...

def results_normalization():
    """
    Normalize data by converting to interval <0-1>
    """
    ...

@app.get("/detect")
def detect():
    # Prepare data from request (optional)
    data_preparation()
    
    results = detection_method()
    
    # Convert results to interval <0-1> (optional)
    results = results_normalization(results)

    return results