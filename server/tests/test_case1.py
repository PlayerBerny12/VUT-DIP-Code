from argparse import ArgumentParser
from os import path, listdir
from time import time
from .test_core import send_request, get_responses, map_responses

def parse_args():
    parser = ArgumentParser()

    parser.add_argument(
        "--real_dir",
        "--real",
        help="Directory containing real data.",
        type=str,        
    )
    parser.add_argument(
        "--fake_dir",
        "--fake",
        help="Directory containing fake data.",
        type=str,
    )
      
    args = parser.parse_args()
    return args

def processFile(filename, directory, file_type, output_file):
    file_path = path.join(directory, filename)
    
    if path.isfile(file_path):
        start_time = time()
        requestID = send_request(filename, file_path)
        responses = get_responses(requestID)
        end_time = time()

        output_file.write(f"{file_type};{map_responses(responses)};{path.getsize(file_path)};{end_time-start_time}\n")      

def main():
    args = parse_args()
    
    with open("output.csv", "a") as output:
        for filename in listdir(args.real_dir):
            processFile(filename, args.real_dir, 1, output)

        for filename in listdir(args.fake_dir):
            processFile(filename, args.fake_dir, 0, output)

if __name__ == "__main__":
    main()