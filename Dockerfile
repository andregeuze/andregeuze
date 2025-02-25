FROM ruby:3.2
RUN apt-get update -qq && apt-get install -y build-essential nodejs npm bundler
WORKDIR /app
COPY . /app
RUN gem install bundler
RUN bundle install
CMD ["bundle", "exec", "jekyll", "serve", "--host", "0.0.0.0"]
