from argparse import ArgumentParser
from os import path, listdir
from time import time
from .test_core import send_request, get_responses, map_responses

def parse_args():
    parser = ArgumentParser()

    parser.add_argument(
        "--dir",        
        help="Directory containing real data.",
        type=str,        
    )    
      
    args = parser.parse_args()
    return args

def main():
    args = parse_args()
    
    with open("output3.csv", "a") as output:
        start_time = time()
        
        requestIDs = []            
        all_size = 0            
        
        for filename in listdir(args.dir):
            file_path = path.join(args.dir, filename)
                           
            if path.isfile(file_path):
                batch_size += path.getsize(file_path)
                requestIDs.append(send_request(filename, file_path))            
        
        for requestID in requestIDs:
            get_responses(requestID)
                
        end_time = time()
                        
        output.write(f"{all_size};{end_time-start_time}\n")      
            

if __name__ == "__main__":
    main()