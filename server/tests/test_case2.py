from argparse import ArgumentParser
from os import path, listdir
from time import time
from datetime import datetime
from random import shuffle
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
    filenames = listdir(args.dir)
    shuffle(filenames)

    print(f"Start: {datetime.now()}")    

    with open("output2.csv", "w") as output:
        for filenames_batch in batch(filenames, 5):
            start_time = time()
            
            requestIDs = []     
            responses = []       
            audio_batch_size = 0
            video_batch_size = 0
            audio_batch_count = 0
            video_batch_count = 0

            for filename in filenames_batch:
                file_path = path.join(args.dir, filename)            
                
                if path.isfile(file_path):
                    file_extension = path.splitext(file_path)[1]

                    if file_extension == ".mp4":
                        video_batch_count += 1
                        video_batch_size += path.getsize(file_path)
                    elif file_extension == ".wav":
                        audio_batch_count += 1
                        audio_batch_size += path.getsize(file_path)

                    requestIDs.append(send_request(filename, file_path, file_extension))            
        
            for requestID in requestIDs:
                responses.append(get_responses(requestID))
                
            end_time = time()
            
            output.write(f"{audio_batch_size};{audio_batch_count};{video_batch_size};{video_batch_count};{end_time-start_time}\n")
            output.flush()  
    
    print(responses)
    print(f"End: {datetime.now()}")

if __name__ == "__main__":
    main()