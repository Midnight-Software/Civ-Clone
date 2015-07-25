#this is the default makefile for the c engine thing!
CC=clang

# TODO > Detect CYGWIN compile vs yadda compile.
PROJ_ROOT=..
OUTPUT_DIR=$(PROJ_ROOT)/bin
DEPEN_DIR=$(PROJ_ROOT)/Dependencies
CSFML_DIR=$(DEPEN_DIR)/c/CSFML-2.3-windows-32-bit/CSFML
INCLUDE_DIRS=$(CSFML_DIR)/include
#WINDOWS DIRS
#"C:/Program Files (x86)\Microsoft Visual Studio 12.0\VC\include"

INCLUDE_FLAG=-I"$(INCLUDE_DIRS)"

MAIN=myLib
EXE=out
MAIN_NAME=$(MAIN).$(EXE)
MAIN_LOC=$(OUTPUT_DIR)/$(MAIN_NAME)

OUT_FLAG=-o $(MAIN_LOC)

#default action is rebuild.

all:clean $(MAIN_NAME)
	$(MAIN_LOC)

#Rule to ONLY invoke the build process.
build-only:$(MAIN_NAME)

#Rule to make the main executable.
$(MAIN_NAME):
	$(CC) $(INCLUDE_FLAG) main.c $(OUT_FLAG)

#Rule to create the output dir.
$(OUTPUT_DIR):
	mkdir $(OUTPUT_DIR)

clean:$(OUTPUT_DIR)
	rm -f $(OUTPUT_DIR)/*.out
