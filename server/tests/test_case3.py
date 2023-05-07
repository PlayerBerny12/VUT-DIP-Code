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

def main():
    args = parse_args()
    
    print(f"Start: {datetime.now()}")

    with open("output3.csv", "w") as output:
        start_time = time()
        
        requestIDs = []            
        audio_size = 0
        video_size = 0
        audio_count = 0
        video_count = 0
        
        for filename in listdir(args.dir):
            file_path = path.join(args.dir, filename)
                           
            if path.isfile(file_path):
                file_extension = path.splitext(file_path)[1]

                if file_extension == ".mp4":
                    video_count += 1
                    video_size += path.getsize(file_path)
                elif file_extension == ".wav":
                    audio_count += 1
                    audio_size += path.getsize(file_path)
                    
                requestIDs.append(send_request(filename, file_path, path.splitext(file_path)[1]))            
        
        for requestID in requestIDs:
            get_responses(requestID)
                
        end_time = time()
                        
        output.write(f"{audio_size};{audio_count};{video_size};{video_count};{end_time-start_time}\n")    
    
    print(f"End: {datetime.now()}")

if __name__ == "__main__":
    main()