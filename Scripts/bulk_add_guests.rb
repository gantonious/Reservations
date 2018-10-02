require 'optparse'
require 'csv'
require 'net/http'
require 'json'

def upload_guests(config)
    guests = []
    CSV.foreach(config[:csv_file]) do |row|
        guests << {
            "name" => row[0],
            "maxGuests" => row[1].to_i
        }
    end

    uri = URI('https://api.georgeandjessica.ca/v1/admin/guests/bulk')
    req = Net::HTTP::Post.new(uri, 'Content-Type' => 'application/json')
    req['Authorization'] = config[:token]
    req.body = guests.to_json
    http = Net::HTTP.new(uri.hostname, uri.port)
    http.use_ssl = true
    res = http.request(req)
    puts res
end

def parse_args(args)
    config = {}

    option_parser = OptionParser.new do |options|
        options.on("-t", "--token [TOKEN]",
            "Token used to authenticate to reservations") do |token|
            config[:token] = token
        end

        options.on("-f", "--file [CSV FILE]",
            "File with guests information") do |csv_file|
            config[:csv_file] = csv_file
        end

        options.on_tail("-h", "--help", "Show this message") do
            puts options
            exit
        end
    end

    option_parser.parse(args)
    config
end

config = parse_args(ARGV)
upload_guests(config)