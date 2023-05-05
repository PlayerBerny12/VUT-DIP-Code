from argparse import ArgumentParser
from os import path, listdir
from time import time
from datetime import datetime
from test_core import send_request, get_responses, map_responses

def parse_args():
    parser = ArgumentParser()

    parser.add_argument(
        "--dir",        
        help="Directory containing real data.",
        type=str,        
    )    
      
    args = parser.parse_args()
    return args

def batch(iterable, batch_size=1):
    length = len(iterable)

    for index in range(0, length, batch_size):
        yield iterable[index:min(index + batch_size, length)]

def main():
    args = parse_args()
    
    print(f"Start: {datetime.now()}")    

    with open("output2.csv", "w") as output:
        for filenames in batch(listdir(args.dir), 5):
            start_time = time()
            
            requestIDs = []            
            batch_size = 0

            for filename in filenames:
                file_path = path.join(args.dir, filename)            
                
                if path.isfile(file_path):
                    batch_size += path.getsize(file_path)
                    requestIDs.append(send_request(filename, file_path))            
        
            for requestID in requestIDs:
                get_responses(requestID)
                
            end_time = time()
                
            for _ in requestIDs:
                output.write(f"{batch_size};{end_time-start_time}\n")      
    
    print(f"End: {datetime.now()}")

if __name__ == "__main__":
    main()